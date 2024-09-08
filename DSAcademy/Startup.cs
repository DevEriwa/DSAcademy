using Core.Config;
using Core.Db;
using Core.Models;
using Hangfire;
using Hangfire.Common;
using Hangfire.Dashboard;
using Hangfire.States;
using Hangfire.Storage;
using Logic;
using Logic.Helpers;
using Logic.IHelpers;
using Logic.SmtpMailServices;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RedisCache;
using System.Net;
using System.Security.Claims;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<FormOptions>(options =>
        {
            // Set the maximum allowed size for uploaded files to 100 MB (104857600 bytes)
            options.MultipartBodyLengthLimit = 104857600; // 100 MB, adjust as necessary
        });
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("StudentManagement") ?? throw new InvalidOperationException("Connection string 'StudentManagement' not found.")));

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 3;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<AppDbContext>();

        services.AddHttpContextAccessor();

        var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>() ?? throw new InvalidOperationException("Email configuration not found.");
        services.AddSingleton<IEmailConfiguration>(emailConfig);

        var generalConfig = Configuration.GetSection("GeneralConfiguration").Get<GeneralConfiguration>() ?? throw new InvalidOperationException("General configuration not found.");
        services.AddSingleton<IGeneralConfiguration>(generalConfig);

        services.AddTransient<IEmailService, EmailService>();
        services.AddControllersWithViews();

        services.Configure<FormOptions>(x =>
        {
            x.ValueLengthLimit = int.MaxValue;
            x.MultipartBodyLengthLimit = int.MaxValue;
            x.MultipartHeadersLengthLimit = int.MaxValue;
        });

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(20);
        });

        services.AddScoped<IUserHelper, UserHelper>()
                .AddScoped<IEmailHelper, EmailHelper>()
                .AddScoped<IApplicationHelper, ApplicationHelper>()
                .AddScoped<IDropdownHelper, DropdownHelper>()
                .AddScoped<IStudentHelper, StudentHelper>()
                .AddScoped<IAdminHelper, AdminHelper>()
                .AddScoped<ICacheService, RedisCacheService>()
                .AddScoped<IBaseHelper, BaseHelper>()
                .AddScoped<ISuperAdminHelper, SuperAdminHelper>()
                .AddScoped<INotificationHelper, NotificationHelper>()
                .AddScoped<IPaystackHelper, PaystackHelper>();


		//services.AddSession();
		services.AddHttpContextAccessor();
        // Add Hangfire services.  
        services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("HangfireManagement")));
        GlobalJobFilters.Filters.Add(new ExpirationTimeAttribute());
        services.AddSession(options =>
        {
            options.Cookie.Name = ".AdventureWorks.Session";
            options.IdleTimeout = TimeSpan.FromMinutes(60);
            options.Cookie.IsEssential = true;
        });
        services.AddHttpContextAccessor();
        services.Configure<FormOptions>(x =>
        {
            x.ValueLengthLimit = int.MaxValue;
            x.MultipartBodyLengthLimit = int.MaxValue;
            x.MultipartHeadersLengthLimit = int.MaxValue;
        });

        services.AddControllers().AddNewtonsoftJson(x =>
            x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
         );
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseStatusCodePages(async context =>
        {
            var response = context.HttpContext.Response;

            if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                response.Redirect("/Account/Login");
            }

            if (response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                response.Redirect("/Home/Error");
            }

            if (response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                response.Redirect("/Home/Error");
            }

            if (response.StatusCode == (int)HttpStatusCode.BadGateway)
            {
                response.Redirect("/Home/BadRequest");
            }

            if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                response.Redirect("/Home/Error");
            }
        });

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseCookiePolicy();
        app.UseAuthentication();
        app.UseSession();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        UpdateDatabase(app);
    }
	public void HangFireConfiguration(IApplicationBuilder app)
	{
		app.UseHangfireDashboard("/DSAcademyHangfire", new DashboardOptions
		{
			Authorization = new[] { new MyAuthorizationFilter() }
		});

		var robotOptions = new BackgroundJobServerOptions
		{
			ServerName = String.Format(
				"{0}.{1}",
				Environment.MachineName,
				Guid.NewGuid().ToString())
		};
		app.UseHangfireServer(robotOptions);
	}

	public class MyAuthorizationFilter : IDashboardAuthorizationFilter
	{
		public bool Authorize(DashboardContext context)
		{
			var user = context.GetHttpContext().User;
			if (user != null && user.Identity.IsAuthenticated && user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "SuperAdmin"))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	};
	public class ExpirationTimeAttribute : JobFilterAttribute, IApplyStateFilter
	{
		public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
		{
			context.JobExpirationTimeout = TimeSpan.FromDays(20);
		}

		public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
		{
			context.JobExpirationTimeout = TimeSpan.FromDays(20);
		}
	}

	private static void UpdateDatabase(IApplicationBuilder app)
    {
        AppHttpContext.Services = app.ApplicationServices;
        using (var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
    }
}

using Core.Config;
using Core.Db;
using Core.Models;
using Hangfire;
using Hangfire.Common;
using Hangfire.Dashboard;
using Hangfire.States;
using Hangfire.Storage;
using Logic.Helpers;
using Logic.IHelpers;
using Logic.SmtpMailServices;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RedisCache;
using Serilog;
using System.Net;
using System.Security.Claims;
using Log = Serilog.Log;

// ─── Serilog Bootstrap Logger ──────────────────────────────────────────────
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/dsacademy-.log", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    Log.Information("DSAcademy starting up...");

    var builder = WebApplication.CreateBuilder(args);

    // ─── Serilog ───────────────────────────────────────────────────────────
    builder.Host.UseSerilog();

    // ─── Database ──────────────────────────────────────────────────────────
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("StudentManagement")
            ?? throw new InvalidOperationException("Connection string 'StudentManagement' not found.")));

    // ─── Identity (production-grade password policy) ───────────────────────
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 1;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

    // ─── HTTP Context ──────────────────────────────────────────────────────
    builder.Services.AddHttpContextAccessor();

    // ─── Email & General Config ────────────────────────────────────────────
    var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>()
        ?? throw new InvalidOperationException("Email configuration not found.");
    builder.Services.AddSingleton<IEmailConfiguration>(emailConfig);

    var generalConfig = builder.Configuration.GetSection("GeneralConfiguration").Get<GeneralConfiguration>()
        ?? throw new InvalidOperationException("General configuration not found.");
    builder.Services.AddSingleton<IGeneralConfiguration>(generalConfig);

    // ─── Application Services (DI) ─────────────────────────────────────────
    builder.Services.AddTransient<IEmailService, EmailService>();
    builder.Services.AddScoped<IUserHelper, UserHelper>()
                    .AddScoped<IEmailHelper, EmailHelper>()
                    .AddScoped<IApplicationHelper, ApplicationHelper>()
                    .AddScoped<IDropdownHelper, DropdownHelper>()
                    .AddScoped<IStudentHelper, StudentHelper>()
                    .AddScoped<IAdminHelper, AdminHelper>()
                    .AddScoped<ICacheService, RedisCacheService>()
                    .AddScoped<IBaseHelper, BaseHelper>()
                    .AddScoped<ISuperAdminHelper, SuperAdminHelper>()
                    .AddScoped<INotificationHelper, NotificationHelper>()
                    .AddScoped<IPaystackHelper, PaystackHelper>()
                    .AddScoped<ITenantService, TenantService>(); // ← NEW: multi-tenancy

    // ─── Session (single clean registration) ──────────────────────────────
    builder.Services.AddSession(options =>
    {
        options.Cookie.Name = ".DSAcademy.Session";
        options.IdleTimeout = TimeSpan.FromMinutes(60);
        options.Cookie.IsEssential = true;
        options.Cookie.HttpOnly = true;
    });

    // ─── File Upload Limits ────────────────────────────────────────────────
    builder.Services.Configure<FormOptions>(x =>
    {
        x.ValueLengthLimit = int.MaxValue;
        x.MultipartBodyLengthLimit = 104857600; // 100 MB
        x.MultipartHeadersLengthLimit = int.MaxValue;
    });

    // ─── MVC + JSON ────────────────────────────────────────────────────────
    builder.Services.AddControllersWithViews()
        .AddNewtonsoftJson(x =>
            x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    // ─── Hangfire ──────────────────────────────────────────────────────────
    builder.Services.AddHangfire(x =>
        x.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireManagement")));
    builder.Services.AddHangfireServer();
    GlobalJobFilters.Filters.Add(new ExpirationTimeAttribute());

    // ──────────────────────────────────────────────────────────────────────
    var app = builder.Build();
    // ──────────────────────────────────────────────────────────────────────

    // ─── Error Handling ────────────────────────────────────────────────────
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    // ─── Status Code Redirects ─────────────────────────────────────────────
    app.UseStatusCodePages(async context =>
    {
        var response = context.HttpContext.Response;
        if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
            response.Redirect("/Account/Login");
        else if (response.StatusCode == (int)HttpStatusCode.NotFound)
            response.Redirect("/Home/Error");
        else if (response.StatusCode == (int)HttpStatusCode.Forbidden)
            response.Redirect("/Home/Error");
        else if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
            response.Redirect("/Home/Error");

        await Task.CompletedTask;
    });

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseCookiePolicy();
    app.UseAuthentication();
    app.UseSession();
    app.UseAuthorization();

    // ─── Hangfire Dashboard (SuperAdmin only) ──────────────────────────────
    app.UseHangfireDashboard("/DSAcademyHangfire", new DashboardOptions
    {
        Authorization = new[] { new HangfireAuthorizationFilter() }
    });

    // ─── Routes ────────────────────────────────────────────────────────────
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // ─── Auto-migrate DB on startup ────────────────────────────────────────
    try
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        Log.Information("Database migration applied successfully.");
    }
    catch (Exception ex)
    {
        Log.Warning(ex, "Auto-migrate skipped (design-time or DB unavailable).");
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "DSAcademy failed to start.");
}
finally
{
    Log.CloseAndFlush();
}

// ─── Hangfire Auth Filter ──────────────────────────────────────────────────
public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var user = context.GetHttpContext().User;
        return user != null
            && user.Identity!.IsAuthenticated
            && user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "SuperAdmin");
    }
}

// ─── Hangfire Job Expiration ───────────────────────────────────────────────
public class ExpirationTimeAttribute : JobFilterAttribute, IApplyStateFilter
{
    public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        => context.JobExpirationTimeout = TimeSpan.FromDays(20);

    public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        => context.JobExpirationTimeout = TimeSpan.FromDays(20);
}

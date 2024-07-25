using Core.Config;
using Core.Db;
using Core.Models;
using Logic.Helpers;
using Logic.IHelpers;
using Logic.SmtpMailServices;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentManagement") ?? throw new InvalidOperationException("Connection string 'StudentManagement' not found.")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>();

var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>() ?? throw new InvalidOperationException("Email configuration not found.");
builder.Services.AddSingleton<IEmailConfiguration>(emailConfig);

var generalConfig = builder.Configuration.GetSection("GeneralConfiguration").Get<GeneralConfiguration>() ?? throw new InvalidOperationException("General configuration not found.");
builder.Services.AddSingleton<IGeneralConfiguration>(generalConfig);

builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddControllersWithViews();

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.MultipartHeadersLengthLimit = int.MaxValue;
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

builder.Services.AddScoped<IUserHelper, UserHelper>()
    .AddScoped<IEmailHelper, EmailHelper>()
    .AddScoped<IApplicationHelper, ApplicationHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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

app.Run();

static void UpdateDatabase(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}

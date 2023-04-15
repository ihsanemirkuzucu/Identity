using AspNetCoreIdentityApp.Web.ClaimProvider;
using AspNetCoreIdentityApp.Web.Extensions;
using AspNetCoreIdentityApp.Repository.Models;
using AspNetCoreIdentityApp.Core.OptionsModels;
using AspNetCoreIdentityApp.Core.Permissions;
using AspNetCoreIdentityApp.Web.Requirenment;
using AspNetCoreIdentityApp.Web.Seeds;
using AspNetCoreIdentityApp.Service.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"), options =>
    {
        options.MigrationsAssembly("AspNetCoreIdentityApp.Repository");
    });
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(30);
});

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddIdentityWithExt();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IClaimsTransformation, UserClaimProviders>();
builder.Services.AddScoped<IAuthorizationHandler, ExchangeExpireRequirenmentHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ViolenceRequirenmentHandler>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AnkaraPolicy", policy =>
    {
        policy.RequireClaim("city", "Ankara");
    });

    options.AddPolicy("ExchangePolicy", policy =>
    {
        policy.AddRequirements(new ExchangeExpireRequirenment());
    });

    options.AddPolicy("ViolencePolicy", policy =>
    {
        policy.AddRequirements(new ViolenceRequirenment() { ThresholdAge = 18 });
    });

    options.AddPolicy("OrderPermissionReadAndDelete", policy =>
    {
        policy.RequireClaim("Permission", PermissionsRoot.Order.Read);
        policy.RequireClaim("Permission", PermissionsRoot.Order.Delete);
        policy.RequireClaim("Permission", PermissionsRoot.Stock.Delete);
    });

    options.AddPolicy("PermissionsRoot.Order.Read", policy =>
    {
        policy.RequireClaim("Permission", PermissionsRoot.Order.Read);

    });

    options.AddPolicy("PermissionsRoot.Order.Delete", policy =>
    {
        policy.RequireClaim("Permission", PermissionsRoot.Order.Delete);
    });

    options.AddPolicy("PermissionsRoot.Stock.Delete", policy =>
    {
        policy.RequireClaim("Permission", PermissionsRoot.Stock.Delete);
    });
});

builder.Services.ConfigureApplicationCookie(opt =>
{
    var cookieBuilder = new CookieBuilder();
    cookieBuilder.Name = "AppCookie";
    opt.LoginPath = new PathString("/Home/SignIn");
    opt.LogoutPath = new PathString("/Member/Logout");
    opt.AccessDeniedPath = new PathString("/Member/AccessDenied");
    opt.Cookie = cookieBuilder;
    opt.ExpireTimeSpan = TimeSpan.FromDays(30);
    opt.SlidingExpiration = true;
});


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    await PermissionSeed.Seed(roleManager);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

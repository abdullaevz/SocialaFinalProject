using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.Abstractions.Services;
using SocialApp.APPLICATION.Cloudinary;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.INFRASTRUCTURE;
using SocialApp.INFRASTRUCTURE.AppSettings;
using SocialApp.INFRASTRUCTURE.Concretes.Servcies;
using SocialApp.INFRASTRUCTURE.SignalUserConfiguration;
using SocialApp.PERSISTENCE;
using SocialApp.PERSISTENCE.Concretes.Repositories;
using SocialApp.PERSISTENCE.Concretes.Services;
using SocialApp.PERSISTENCE.Contexts;
using System.Configuration;
using SocialApp.APPLICATION;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied";
});

//Persistence 
builder.Services.AddPersistenceServices(builder.Configuration);

//Infrastructure
builder.Services.AddInfrastructureServices(builder.Configuration);

//Application
builder.Services.AddAplicationServices();

var app = builder.Build();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); 
    app.UseHsts();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

app.MapControllerRoute(name: "Default", pattern:"{Controller=Home}/{Action=Index}");

using (var scope = app.Services.CreateScope())
{
    var startup = scope.ServiceProvider.GetRequiredService<IStartupService>();
    await startup.CreateAdminAndRoles();
    await startup.CreateDefaultAuthRestric();
    await startup.CreateDefaultBanner();
}
app.MapHub<ChatService>("/chat");

app.Run();

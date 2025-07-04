using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialApp.APPLICATION.Abstractions.Services;
using SocialApp.APPLICATION.Cloudinary;
using SocialApp.INFRASTRUCTURE.Concretes.Servcies;
using SocialApp.INFRASTRUCTURE.SignalUserConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.INFRASTRUCTURE;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddSignalR();

        services.Configure<CloudinarySettings>(
            configuration.GetSection("CloudinarySettings"));

        services.AddScoped<IModerationService,ModerationService>();

        services.AddScoped<ICloudUploadService, CloudUploadService>();
        services.AddTransient<IEmailSender, EmailService>();
        services.AddSingleton<IUserIdProvider, UserIdProvider>();

        return services;
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.Abstractions.Services;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.PERSISTENCE.Concretes.Repositories;
using SocialApp.PERSISTENCE.Concretes.Services;
using SocialApp.PERSISTENCE.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.PERSISTENCE;

public static class ServiceRegistration
{
    
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
    op => { op.UseNpgsql(configuration.GetConnectionString("default")); }
    );

        services.AddScoped<IStartupService,StartupService>();
        services.AddScoped<IWriteRepository<Post>, WriteRepository<Post>>();
        services.AddScoped<IWriteRepository<SocialAccount>, WriteRepository<SocialAccount>>();
        services.AddScoped<IWriteRepository<Friend>, WriteRepository<Friend>>();
        services.AddScoped<IWriteRepository<Comment>, WriteRepository<Comment>>();
        services.AddScoped<IWriteRepository<PostLike>, WriteRepository<PostLike>>();
        services.AddScoped<IWriteRepository<PostSave>, WriteRepository<PostSave>>();
        services.AddScoped<IWriteRepository<UserBlock>, WriteRepository<UserBlock>>();
        services.AddScoped<IWriteRepository<Message>, WriteRepository<Message>>();
        services.AddScoped<IReadRepository<Post>, ReadRepository<Post>>();
        services.AddScoped<IReadRepository<Message>, ReadRepository<Message>>();
        services.AddScoped<IReadRepository<PostLike>, ReadRepository<PostLike>>();
        services.AddScoped<IReadRepository<Comment>, ReadRepository<Comment>>();
        services.AddScoped<IReadRepository<UserBlock>, ReadRepository<UserBlock>>();
        services.AddScoped<IReadRepository<Banner>, ReadRepository<Banner>>();
        services.AddScoped<IReadRepository<AuthRestriction>, ReadRepository<AuthRestriction>>();
        services.AddScoped<IReadRepository<SocialAccount>, ReadRepository<SocialAccount>>();
        services.AddScoped<IReadRepository<Friend>, ReadRepository<Friend>>();
        services.AddScoped<IWriteRepository<Banner>,WriteRepository<Banner>>();
        services.AddScoped<IWriteRepository<AuthRestriction>,WriteRepository<AuthRestriction>>();
        services.AddScoped<IReadRepository<PostSave>, ReadRepository<PostSave>>();

        services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


        return services;
    }
}

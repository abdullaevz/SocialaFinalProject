using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.Abstractions.Services;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.PERSISTENCE.Concretes.Services;

public class StartupService : IStartupService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<AppUser> _singInManager;
    private readonly IWriteRepository<Banner> _bannerWriteRepo;
    private readonly IReadRepository<Banner> _bannerReadRepo;
    private readonly IWriteRepository<AuthRestriction> _authWriteRepo;
    private readonly IReadRepository<AuthRestriction> _authReadRepo;
    private readonly string _defaultPfp;
    private readonly string _defaultBanner;

    public StartupService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> singInManager, IMapper mapper, IWriteRepository<Banner> bannerWriteRepo, IWriteRepository<AuthRestriction> authWriteRepo, IConfiguration configuration, IReadRepository<Banner> bannerReadRepo, IReadRepository<AuthRestriction> authReadRepo)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _singInManager = singInManager;
        _bannerWriteRepo = bannerWriteRepo;
        _authWriteRepo = authWriteRepo;
        _defaultBanner = configuration["DefaultLinks:DefaultBanner"] ?? "";
        _defaultPfp = configuration["DefaultLinks:DefaultProfilePhoto"] ?? "";
        _bannerReadRepo = bannerReadRepo;
        _authReadRepo = authReadRepo;
    }
    public async Task CreateAdminAndRoles()
    {

        if (!await _roleManager.RoleExistsAsync("Member"))
        {
            AppRole memberRole = new AppRole() { Name = "Member" };
            await _roleManager.CreateAsync(memberRole);
        }

        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            AppRole adminRole = new AppRole() { Name = "Admin" };
            await _roleManager.CreateAsync(adminRole);
        }

        if (await _userManager.FindByEmailAsync("admin@gmail.com") is null)
        {
            var adminUser = new AppUser()
            {
                Email = "admin@gmail.com",
                UserName = "admin",
                DefaultProfilePath = _defaultPfp,
                ProfilePhotoPath = _defaultPfp,
                Fullname = "Admin user",
                Role = DOMAIN.Enums.UserRoles.ADMIN,
                CreatedDate = DateTime.UtcNow,
            };

            await _userManager.CreateAsync(adminUser, "Admin.123");
            var result = await _userManager.AddToRoleAsync(adminUser, "Admin");

            foreach (var item in result.Errors)
            {
                Console.WriteLine(item.Description);
            }
        }
    }

    public async Task CreateDefaultAuthRestric()
    {
        var model = _authReadRepo.Table.FirstOrDefault();
        if (model is null)
        {
            await _authWriteRepo.CreateAsync(new AuthRestriction() { LoginActionIsActive = true, RegisterActionIsActive = true, CreationDate = DateTime.UtcNow });
        }
    }

    public async Task CreateDefaultBanner()
    {
        var model = _bannerReadRepo.Table.FirstOrDefault();
        if (model is null)
        {
            await _bannerWriteRepo.CreateAsync(new Banner() { Url = _defaultBanner, IsActive = true, CreationDate = DateTime.UtcNow });
        }
    }
}

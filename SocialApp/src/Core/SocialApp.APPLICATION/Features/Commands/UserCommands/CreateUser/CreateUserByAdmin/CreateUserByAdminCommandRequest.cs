using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SocialApp.APPLICATION.Abstractions.Services;
using SocialApp.APPLICATION.ViewModels.UserViewModels;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.UserCommands.CreateUser.CreateUserByAdmin;

public class CreateUserByAdminCommandRequest : IRequest<AppResult>
{
    public AdminUserCreateVM AdminUserCreateVM { get; set; }

    public CreateUserByAdminCommandRequest(AdminUserCreateVM adminUserCreateVM)
    {
        AdminUserCreateVM = adminUserCreateVM;
    }
}

public class CreateUserByAdminCommandHandler : IRequestHandler<CreateUserByAdminCommandRequest, AppResult>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ICloudUploadService _cloudUploadService;
    private readonly string _defaultPP;

    public CreateUserByAdminCommandHandler(UserManager<AppUser> userManager, ICloudUploadService cloudUploadService, IConfiguration configuration)
    {
        _userManager = userManager;
        _cloudUploadService = cloudUploadService;
        _defaultPP = configuration["DefaultLinks:DefaultProfilePhoto"] ?? "empty";

    }

    public async Task<AppResult> Handle(CreateUserByAdminCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.AdminUserCreateVM is null)
        {
            throw new UserException("User create model is null");
        }

        if (string.IsNullOrEmpty(request.AdminUserCreateVM.Password))
        {
            return await AppResult.Failure("Enter valid password");
        }

        var checkModel = await _userManager.FindByEmailAsync(request.AdminUserCreateVM.Email);

        if (checkModel is not null)
        {
            return await AppResult.Failure("This email adress is already taken");
        }

        AppUser user = new()
        {
            Fullname = request.AdminUserCreateVM.Fullname,
            Email = request.AdminUserCreateVM.Email,
            PhoneNumber = request.AdminUserCreateVM.PhoneNumber,
            UserName = request.AdminUserCreateVM.UserName,
            Description = request.AdminUserCreateVM.Description,
            Country = request.AdminUserCreateVM.Country,
            Profession = request.AdminUserCreateVM.Profession,
            IsPrivate = request.AdminUserCreateVM.IsPrivate,
            IsVerified = request.AdminUserCreateVM.IsVerified,
            AcceptPolicyStatus = request.AdminUserCreateVM.AcceptPolicyStatus,
            Role = request.AdminUserCreateVM.Role,
            SecurityStatus = request.AdminUserCreateVM.SecurityStatus,
            CreatedDate= DateTime.UtcNow
        };

        //File check and 
        if (request.AdminUserCreateVM.File is not null)
        {
            var file = request.AdminUserCreateVM.File;

            string name = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            string newFileName = $"{name}-{Guid.NewGuid().ToString()}-{extension}";


            var uploadResult = await _cloudUploadService.UploadPhotoAsync(file, newFileName);

            if (!uploadResult.Success)
            {
                return await AppResult.Failure("Something went wrong while proccessing upload photo");
            }

            user.ProfilePhotoPath = uploadResult.Message;
        }
        else
        {
            user.ProfilePhotoPath = _defaultPP;
            user.DefaultProfilePath = _defaultPP;
        }

        var result = await _userManager.CreateAsync(user, request.AdminUserCreateVM.Password);
        if (!result.Succeeded)
        {
            return await AppResult.Failure(result.Errors.Select(e => e.Description).ToArray());
        }

        return await AppResult.SuccessResult("User created succsesfully");
    }
}

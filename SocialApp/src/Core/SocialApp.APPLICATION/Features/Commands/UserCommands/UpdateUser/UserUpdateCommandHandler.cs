using AutoMapper;
using CloudinaryDotNet;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.Abstractions.Services;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Commands.UserCommands.UpdateUser;

public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommandRequest, AppResult>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ICloudUploadService _cloudUploadService;
    private readonly RoleManager<AppRole> _roleManager;

    public UserUpdateCommandHandler(UserManager<AppUser> userManager, ICloudUploadService cloudUploadService, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _cloudUploadService = cloudUploadService;
        _roleManager = roleManager;
    }

    public async Task<AppResult> Handle(UserUpdateCommandRequest request, CancellationToken cancellationToken)
    {

        if (request.UserModel is null)
        {
            return await AppResult.Failure("Error,Request model is null");
        }


        if (request.UserModel.UserName is null || request.UserModel.Email is null)
        {
            return await AppResult.Failure("Unique values must not be empty.");
        }

        if (request.UserModel.Email.Equals("admin@gmail.com"))
        {
            return await AppResult.Failure("Static admin profile data does not changeable");
        }
        var findUser = await _userManager.FindByEmailAsync(request.UserModel.Email) ?? await _userManager.FindByNameAsync(request.UserModel.UserName);

        if (!request.LocalUser.EmailConfirmed)
        {
            return await AppResult.Failure("You must be confirm your email before update your profile");
        }


        if (findUser is not null)
        {

            if (findUser.Id != request.LocalUser.Id)
            {
                if (findUser.UserName.Equals(request.UserModel.UserName))
                {
                    return await AppResult.Failure("This username already used by another member");
                }
                else if (findUser.Email.Equals(request.UserModel.Email))
                {
                    return await AppResult.Failure("This email adress already used by another member");
                }
            }
            else
            {
                findUser.Fullname = request.UserModel.Fullname?.Trim();
                findUser.UserName = request.UserModel.UserName?.Trim();
                findUser.Email = request.UserModel.Email?.Trim();
                findUser.Country = request.UserModel.Country?.Trim();
                findUser.Profession = request.UserModel.Profession?.Trim();
                findUser.PhoneNumber = request.UserModel.PhoneNumber?.Trim();
                findUser.Description = request.UserModel.Description?.Trim();
                findUser.IsPrivate = request.UserModel.IsPrivate;
                //Add
                findUser.IsVerified = request.UserModel.IsVerified;
                findUser.Role = request.UserModel.Role;

                if (request.UserModel.Role == DOMAIN.Enums.UserRoles.ADMIN)
                {
                    await _userManager.AddToRoleAsync(findUser,"Admin");
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(findUser,"Admin");
                    await _userManager.AddToRoleAsync(findUser, "Member");
                }


            }
        }



        //File check and 
        if (request.UserModel.Image is not null)
        {
            var file = request.UserModel.Image;

            string name = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            string newFileName = $"{name}-{Guid.NewGuid().ToString()}-{extension}";


            var result = await _cloudUploadService.UploadPhotoAsync(file, newFileName);

            if (!result.Success)
            {
                return await AppResult.Failure("Something went wrong while proccessing upload photo");
            }

            findUser.ProfilePhotoPath = result.Message;
        }

        //Update
        var updateResult = await _userManager.UpdateAsync(findUser);


        if (!updateResult.Succeeded)
        {
            return await AppResult.Failure(updateResult.Errors.Select(e => e.Description).ToArray());
        }

        return await AppResult.SuccessResult();
    }

}

using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Commands.UserCommands.ResetPassword;

public class UserResetPasswordCommandHandler : IRequestHandler<UserResetPasswordCommandRequest, AppResult>
{
    private readonly UserManager<AppUser> _userManager;


    public UserResetPasswordCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AppResult> Handle(UserResetPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.UserPasswordUpdateVM is null)
        {
            throw new UserException("Request model for password reset is null");
        }

        if (!request.UserPasswordUpdateVM.NewPassword.Equals(request.UserPasswordUpdateVM.newPasswordConfirm))
        {
            return await AppResult.Failure("New passwords dont match");
        }

        var user = await _userManager.FindByIdAsync(request.UserPasswordUpdateVM.UserId.ToString());

        if (user is null)
        {
            throw new UserException($"Couldnt find any current user for this id {request.UserPasswordUpdateVM.UserId}");
        }
        var result = await _userManager.ChangePasswordAsync(user,request.UserPasswordUpdateVM.CurrentPasswordRaw, request.UserPasswordUpdateVM.NewPassword);

        if (!result.Succeeded)
        {
            return await AppResult.Failure(result.Errors.Select(item=>item.Description).ToArray());
        }

        return await AppResult.SuccessResult("New password successfully saved");
    }
}

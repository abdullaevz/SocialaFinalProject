using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Commands.AppUserCommands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, AppResult>
{
    private readonly UserManager<AppUser> _userManager;

    public DeleteUserCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AppResult> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
    {
        AppUser? user = await _userManager.FindByIdAsync(request.Id.ToString());

        if (user is null)
        {
            return await AppResult.Failure("Cannot find any entity with this id");
        }

        await _userManager.DeleteAsync(user);
        return await AppResult.SuccessResult("User deleted succesfully");
    }
}

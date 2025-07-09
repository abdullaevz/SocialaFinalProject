using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Commands.UserCommands.ReportUser;

public class ReportUserCommandsHandler : IRequestHandler<ReportUserCommandsRequest, AppResult>
{
    private readonly UserManager<AppUser> userManager;

    public ReportUserCommandsHandler(UserManager<AppUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<AppResult> Handle(ReportUserCommandsRequest request, CancellationToken cancellationToken)
    {
        if (request.UserReportVM is null)
        {
            throw new UserException("User report model is null");
        }

        var user = await userManager.FindByIdAsync(request.UserReportVM.UserId.ToString());

        if (user is not null)
        {
            user.SecurityStatus = request.UserReportVM.Status;
            user.ReportCount += 1;
            await userManager.UpdateAsync(user);
        }

        return await AppResult.SuccessResult();
    }
}

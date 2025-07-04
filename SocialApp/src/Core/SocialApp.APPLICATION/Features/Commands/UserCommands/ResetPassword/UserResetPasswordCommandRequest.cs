using MediatR;
using SocialApp.APPLICATION.ViewModels.UserViewModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.UserCommands.ResetPassword;

public class UserResetPasswordCommandRequest : IRequest<AppResult>
{
    public UserPasswordUpdateVM UserPasswordUpdateVM { get; set; }

    public UserResetPasswordCommandRequest(UserPasswordUpdateVM userPasswordUpdateVM)
    {
        UserPasswordUpdateVM = userPasswordUpdateVM;
    }
}

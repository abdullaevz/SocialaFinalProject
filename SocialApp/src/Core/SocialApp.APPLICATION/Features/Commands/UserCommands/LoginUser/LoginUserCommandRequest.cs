using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialApp.APPLICATION.ViewModels.UserViewModels;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.AppUserCommands.LoginUser;

public class LoginUserCommandRequest:IRequest<GenericAppResult<AppUser>>
{
    public UserLoginVM loginModel;

    public LoginUserCommandRequest(UserLoginVM loginModel)
    {
        this.loginModel = loginModel;
    }
}

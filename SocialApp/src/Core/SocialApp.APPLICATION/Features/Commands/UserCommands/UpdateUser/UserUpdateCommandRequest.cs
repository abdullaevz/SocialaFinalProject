using MediatR;
using SocialApp.APPLICATION.Abstractions.Services;
using SocialApp.APPLICATION.ViewModels.UserViewModels;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.UserCommands.UpdateUser;

public class UserUpdateCommandRequest : IRequest<AppResult>
{
    public UserUpdateVM UserModel { get; set; }
    public AppUser LocalUser{ get; set; }

    public UserUpdateCommandRequest(AppUser localUser, UserUpdateVM userModel)
    {
        LocalUser = localUser;
        UserModel = userModel;
    }
}

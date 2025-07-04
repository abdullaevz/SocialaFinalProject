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

namespace SocialApp.APPLICATION.Features.Commands.UserCommands.UpdateUser.UpdateUserByAdmin;

public class UserUpdateByAdminCommandRequest : IRequest<AppResult>
{
    public UserUpdateVM UserModel { get; set; }
    public int UserId { get; set; }

    public UserUpdateByAdminCommandRequest(UserUpdateVM userModel,int userId)
    {
        UserModel = userModel;
        UserId = userId;
    }
}

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

namespace SocialApp.APPLICATION.Features.Commands.UserCommands.CreateUser;

public class CreateUserCommandRequest : IRequest<AppResult>
{
    public UserCreateVM userModel { get; set; }

    public CreateUserCommandRequest(UserCreateVM userModel)
    {
        this.userModel = userModel;
    }
}

using MediatR;
using SocialApp.APPLICATION.ViewModels.UserFriendViewModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.UserFriendCommands.CreateUserFriendCommand;

public class CreateUserFriendCommandRequest:IRequest<AppResult>
{
    public UserFriendCreateVM FriendModel { get; set; }

    public CreateUserFriendCommandRequest(UserFriendCreateVM friendModel)
    {
        FriendModel = friendModel;
    }
}

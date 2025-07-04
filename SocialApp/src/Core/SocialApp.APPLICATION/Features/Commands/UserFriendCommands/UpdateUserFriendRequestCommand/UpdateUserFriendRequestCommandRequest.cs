using MediatR;
using SocialApp.DOMAIN.Enums;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.UserFriendCommands.UpdateUserFriendRequestCommand;

public class UpdateUserFriendRequestCommandRequest : IRequest<AppResult>
{
    public Statuses Result { get; set; }
    public int RequestId { get; set; }
    public int LocalId { get; set; }

    public UpdateUserFriendRequestCommandRequest(Statuses result, int requestId, int localId)
    {
        Result = result;
        RequestId = requestId;
        LocalId = localId;
    }
}

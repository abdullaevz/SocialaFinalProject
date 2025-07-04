using MediatR;
using SocialApp.APPLICATION.ViewModels.UserFriendViewModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.FriendQueries.GetUsersFriendRequestsQueryRequest;

public class RecievedFriendRequestsQueryRequest : IRequest<GenericAppResult<UserFriendRequestGetVM>>
{
    public int ReceieverId { get; set; }

    public RecievedFriendRequestsQueryRequest(int receieverId)
    {
        ReceieverId = receieverId;
    }
}

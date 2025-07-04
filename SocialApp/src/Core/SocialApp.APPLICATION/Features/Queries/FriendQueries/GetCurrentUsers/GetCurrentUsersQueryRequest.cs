using MediatR;
using SocialApp.APPLICATION.ViewModels.UserFriendViewModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.FriendQueries.GetCurrentUsers;

public class GetCurrentUsersQueryRequest : IRequest<GenericAppResult<CurrentFriendModel>>
{
    public int LocalId { get; set; }

    public GetCurrentUsersQueryRequest(int localId)
    {
        LocalId = localId;
    }
}

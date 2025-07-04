using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.FriendQueries.GetSuggestedFriends;

public class SuggestedFriendsQueryRequest:IRequest<GenericAppResult<AppUser>>
{
    public int LocalId { get; set; }

    public SuggestedFriendsQueryRequest(int localId)
    {
        LocalId = localId;
    }
}

public class SuggestedFriendsQueryHandler : IRequestHandler<SuggestedFriendsQueryRequest, GenericAppResult<AppUser>>
{
    private readonly IReadRepository<Friend> _friendRepository;
    private readonly UserManager<AppUser> _userManager;

    public SuggestedFriendsQueryHandler(IReadRepository<Friend> friendRepository, UserManager<AppUser> userManager)
    {
        _friendRepository = friendRepository;
        _userManager = userManager;
    }

    public async Task<GenericAppResult<AppUser>> Handle(SuggestedFriendsQueryRequest request, CancellationToken cancellationToken)
    {
        var userQuery = _userManager.Users.Where(u=>u.IsVerified);
        if (userQuery is not null)
        {
            return new GenericAppResult<AppUser> { Success = true, Data = userQuery.ToList() };
        }

        return await GenericAppResult<AppUser>.Failure("not found");
    }
}

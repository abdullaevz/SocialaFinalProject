using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.ViewModels.UserFriendViewModels;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Queries.FriendQueries.GetUsersFriendRequestsQueryRequest;

public class RecievedFriendReqsHandler : IRequestHandler<RecievedFriendRequestsQueryRequest, GenericAppResult<UserFriendRequestGetVM>>
{
    private readonly IReadRepository<Friend> _readRepository;
    private readonly UserManager<AppUser> _userManager;

    public RecievedFriendReqsHandler(IReadRepository<Friend> readRepository, UserManager<AppUser> userManager)
    {
        _readRepository = readRepository;
        _userManager = userManager;
    }

    public async Task<GenericAppResult<UserFriendRequestGetVM>> Handle(RecievedFriendRequestsQueryRequest request, CancellationToken cancellationToken)
    {

        var requestList = _readRepository.GetByCondition(data => data.RecieverUserId == request.ReceieverId)?.ToList();

        if (requestList is null)
        {
            throw new UserFriendException("Requested friend list is null");
        }

        List<UserFriendRequestGetVM> senders = [];

        foreach (var item in requestList)
        {
            if (item.areFriends)
            {
                continue;
            }
            var user = await _userManager.FindByIdAsync(item.SenderUserId.ToString());
            if (user is not null)
            {
                senders.Add(new UserFriendRequestGetVM()
                {
                    RequestId=item.Id,
                    UserId = user.Id,
                    Fullname = user.Fullname,
                    Username=user.UserName,
                    ProfilePhoto = user.ProfilePhotoPath,
                    DefaultProfilePhoto = user.DefaultProfilePath
                });
            }
        }

        return new GenericAppResult<UserFriendRequestGetVM>() { Success = true, Data = senders };
    }
}

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.ViewModels.UserFriendViewModels;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Queries.FriendQueries.GetCurrentUsers;

public class GetCurrentUsersQueryHandler : IRequestHandler<GetCurrentUsersQueryRequest, GenericAppResult<CurrentFriendModel>>
{
    private readonly IReadRepository<Friend> _readRepository;
    private readonly IReadRepository<Post> _postReadRepo;
    private readonly UserManager<AppUser> _userManager;

    public GetCurrentUsersQueryHandler(IReadRepository<Friend> readRepository, UserManager<AppUser> userManager, IReadRepository<Post> postReadRepo)
    {
        _readRepository = readRepository;
        _userManager = userManager;
        _postReadRepo = postReadRepo;
    }

    public async Task<GenericAppResult<CurrentFriendModel>> Handle(GetCurrentUsersQueryRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new FriendException("Request model is null");
        }

        var friendRelations = _readRepository
      .GetByCondition(m => m.areFriends &&
                           (m.SenderUserId == request.LocalId || m.RecieverUserId == request.LocalId))
      ?.ToList() ?? new List<Friend>();

        var relatedUsersWithFriendId = friendRelations
    .Select(m => new
    {
        UserId = m.SenderUserId == request.LocalId ? m.RecieverUserId : m.SenderUserId,
        FriendRelationId = m.Id
    })
    .ToList();

        var userIds = relatedUsersWithFriendId.Select(x => x.UserId).ToList();

        var users = await _userManager.Users
            .Where(u => userIds.Contains(u.Id)).Include(u=>u.Posts)
            .ToListAsync();

        var finalList = users.Select(user =>
        {
            var friendRelation = relatedUsersWithFriendId.First(x => x.UserId == user.Id);

            var posts = _postReadRepo.GetByCondition(p => p.Id == user.Id)?.ToList();
            return new CurrentFriendModel
            {
                RequestId = friendRelation.FriendRelationId,
                FriendId = friendRelation.UserId,
                Username = user.UserName,
                Fullname = user.Fullname,
                ProfilePhoto = user.ProfilePhotoPath,
                DefaultProfilePhoto = user.DefaultProfilePath,
                IsConfirmed = user.EmailConfirmed,
                PostCount = user.Posts.Count

            };
        }).ToList();


        return new GenericAppResult<CurrentFriendModel> { Success = true, Data = finalList.ToList() };
    }
}

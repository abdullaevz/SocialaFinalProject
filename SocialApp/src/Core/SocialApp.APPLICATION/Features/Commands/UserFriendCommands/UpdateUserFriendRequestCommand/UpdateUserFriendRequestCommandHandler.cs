using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Enums;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Commands.UserFriendCommands.UpdateUserFriendRequestCommand;

public class UpdateUserFriendRequestCommandHandler : IRequestHandler<UpdateUserFriendRequestCommandRequest, AppResult>
{
    private readonly IWriteRepository<Friend> _writeRepository;
    private readonly IReadRepository<Friend> _readRepository;

    public UpdateUserFriendRequestCommandHandler(IWriteRepository<Friend> writeRepository, IReadRepository<Friend> readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<AppResult> Handle(UpdateUserFriendRequestCommandRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new FriendException("Friend Request model is null");
        }

        if (request.Result == Statuses.ACCEPT)
        {
            var model = await _readRepository.GetByIdAsync(request.RequestId);
            if (model is not null)
            {


                model.areFriends = true;
                await _writeRepository.SaveAsync();
                return await AppResult.SuccessResult("Friend request successfully accepted");
            }
        }
        else if (request.Result == Statuses.REJECT)
        {
            var model = await _readRepository.GetByIdAsync(request.RequestId);
            if (model is not null)
            {
                await _writeRepository.DeleteAsync(model);
                return await AppResult.SuccessResult("Friend request successfully rejected");

            }
        }
        return await AppResult.SuccessResult("Friend request successfully rejected");
    }
}

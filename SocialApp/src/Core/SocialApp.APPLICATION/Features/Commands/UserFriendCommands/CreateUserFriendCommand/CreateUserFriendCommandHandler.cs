using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Commands.UserFriendCommands.CreateUserFriendCommand;

public class CreateUserFriendCommandHandler : IRequestHandler<CreateUserFriendCommandRequest, AppResult>
{
    private readonly IWriteRepository<Friend> _writeRepository;
    private readonly IReadRepository<Friend> _readRepository;

    public CreateUserFriendCommandHandler(IWriteRepository<Friend> writeRepository, IReadRepository<Friend> readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<AppResult> Handle(CreateUserFriendCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.FriendModel is null)
        {
            throw new UserFriendException("Request model is null");
        }

        Friend friendRequest = new Friend()
        {
            SenderUserId = request.FriendModel.SenderId,
            RecieverUserId = request.FriendModel.RecieverId,
            areFriends = false,
        };

        var firstVersion = _readRepository.GetByCondition(m => m.RecieverUserId == request.FriendModel.RecieverId && m.SenderUserId == request.FriendModel.SenderId)?.SingleOrDefault();
        var secondVersion = _readRepository.GetByCondition(m => m.RecieverUserId == request.FriendModel.SenderId && m.SenderUserId == request.FriendModel.RecieverId)?.SingleOrDefault();

        if (firstVersion is null && secondVersion is null)
        {
            await _writeRepository.CreateAsync(friendRequest);
        }


        return await AppResult.SuccessResult("Request sended succesfully");
    }
}

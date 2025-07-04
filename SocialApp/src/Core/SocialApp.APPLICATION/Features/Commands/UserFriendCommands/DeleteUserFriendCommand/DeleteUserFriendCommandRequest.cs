using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.UserFriendCommands.DeleteUserFriendCommand;

public class DeleteUserFriendCommandRequest:IRequest<AppResult>
{
    public int LocalId { get; set; }
    public int UserId { get; set; }

    public DeleteUserFriendCommandRequest(int localId, int userId)
    {
        LocalId = localId;
        UserId = userId;
    }

}

public class DeleteUserFriendCommandHandler : IRequestHandler<DeleteUserFriendCommandRequest, AppResult>
{
    private readonly IReadRepository<Friend> _friendsRepository;
    private readonly IWriteRepository<Friend> _friendsWriteRepository;
    public DeleteUserFriendCommandHandler(IReadRepository<Friend> friendsRepository, IWriteRepository<Friend> friendsWriteRepository)
    {
        _friendsRepository = friendsRepository;
        _friendsWriteRepository = friendsWriteRepository;
    }

    public async Task<AppResult> Handle(DeleteUserFriendCommandRequest request, CancellationToken cancellationToken)
    {
        var model = _friendsRepository.GetByCondition(
            f=>(f.SenderUserId==request.LocalId&&f.RecieverUserId==request.UserId)
            ||
            (f.SenderUserId==request.UserId&&f.RecieverUserId==request.LocalId)
            )?.FirstOrDefault();

        if (model is not null)
        {
            await _friendsWriteRepository.DeleteAsync(model);
        }

        return await AppResult.SuccessResult();
    }
}

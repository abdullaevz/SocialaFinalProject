using AutoMapper.Configuration.Annotations;
using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.FriendQueries.GetUsersFriendStatusQuery;

public class GetUsersFriendStatusQueryRequest : IRequest<AppResult>
{
    public int RecieverId { get; set; }
    public int SenderId { get; set; }


    public GetUsersFriendStatusQueryRequest(int recieverId, int senderId)
    {
        RecieverId = recieverId;
        SenderId = senderId;
    }

}

public class GetUsersFriendStatusQueryHandler : IRequestHandler<GetUsersFriendStatusQueryRequest, AppResult>
{
    private readonly IReadRepository<Friend> _readRepository;

    public GetUsersFriendStatusQueryHandler(IReadRepository<Friend> readRepository)
    {
        _readRepository = readRepository;
    }


    public async Task<AppResult> Handle(GetUsersFriendStatusQueryRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new FriendException("Friend request model is null");
        }

        var firstVersion = _readRepository.GetByCondition(m => m.RecieverUserId == request.RecieverId && m.SenderUserId == request.SenderId)?.SingleOrDefault();
        var secondVersion = _readRepository.GetByCondition(m => m.RecieverUserId == request.SenderId && m.SenderUserId == request.RecieverId)?.SingleOrDefault();

        if (firstVersion is not null)
        {
            return new AppResult() { Success = true, Response = firstVersion.areFriends };
        }
        else if (secondVersion is not null)
        {
            return new AppResult() { Success = true, Response = secondVersion.areFriends };
        }

        return new AppResult() { Success = false, Response = false };
    }
}

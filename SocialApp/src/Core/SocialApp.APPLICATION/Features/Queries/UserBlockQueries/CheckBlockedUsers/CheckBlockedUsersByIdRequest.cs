using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.UserBlockQueries.CheckBlockedUsers;

public class CheckBlockedUsersByIdRequest:IRequest<GenericAppResult<int>>
{
    public int LocalUserId{ get; set; }

    public CheckBlockedUsersByIdRequest(int localUserId)
    {
        LocalUserId = localUserId;
    }
}

public class CheckBlockedUsersByIdHandler : IRequestHandler<CheckBlockedUsersByIdRequest, GenericAppResult<int>>
{
    private readonly IReadRepository<UserBlock> readRepository;

    public CheckBlockedUsersByIdHandler(IReadRepository<UserBlock> readRepository)
    {
        this.readRepository = readRepository;
    }

    public async Task<GenericAppResult<int>> Handle(CheckBlockedUsersByIdRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new Exception("Request model is null");
        }

        var blockList = readRepository.GetByCondition(b => b.BlockedBy == request.LocalUserId) ;

        if (blockList is not null)
        {
            List<int> blockedUsers = blockList.Select(b=>b.BlockedUser).ToList();
            return new GenericAppResult<int>() { Success = true, Data = blockedUsers };
        }

        return await GenericAppResult<int>.Failure("Empty");

    }
}

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

namespace SocialApp.APPLICATION.Features.Queries.UserQueries.GetUserByUsername;

public class GetUserByUsernameQueryRequest : IRequest<GenericAppResult<AppUser>>
{
    public string UserName { get; set; }
    public int LocalUserId { get; set; }

    public GetUserByUsernameQueryRequest(string userName, int localUserId)
    {
        UserName = userName;
        LocalUserId = localUserId;
    }
}

public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQueryRequest, GenericAppResult<AppUser>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IReadRepository<UserBlock> readRepository;

    public GetUserByUsernameQueryHandler(UserManager<AppUser> userManager, IReadRepository<UserBlock> readRepository)
    {
        _userManager = userManager;
        this.readRepository = readRepository;
    }

    public async Task<GenericAppResult<AppUser>> Handle(GetUserByUsernameQueryRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user is null)
        {
            return await GenericAppResult<AppUser>.Failure($"Not found any user with this username {request.UserName}");
        }

        var blockResult = readRepository.GetByCondition(b=>b.BlockedBy==user.Id&&b.BlockedUser==request.LocalUserId)?.FirstOrDefault();
        if (blockResult is not null)
        {
            return await GenericAppResult<AppUser>.Failure("This user is blocked you");
        }

        return new GenericAppResult<AppUser> { Success = true, OneData = user };
    }
}

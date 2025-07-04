using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.UserQueries.GetAllUsers;

public class GetAllUsersQueryRequest : IRequest<GenericAppResult<AppUser>>
{
}

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GenericAppResult<AppUser>>
{
    private readonly UserManager<AppUser> _userManager;

    public GetAllUsersQueryHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<GenericAppResult<AppUser>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
    {
        var allUsers = _userManager.Users;
        return new GenericAppResult<AppUser>()
        {
            Success = true,
            Message = "Succesfull",
            Data = allUsers.ToList()
        };
    }
}

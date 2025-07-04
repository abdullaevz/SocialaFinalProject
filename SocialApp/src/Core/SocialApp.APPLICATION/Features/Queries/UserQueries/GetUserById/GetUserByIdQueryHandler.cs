using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.UserQueries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, GenericAppResult<AppUser>>
{
    private readonly UserManager<AppUser> _userManager;

    public GetUserByIdQueryHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<GenericAppResult<AppUser>> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var userQuery = _userManager.Users.Where(u=>u.Id==request.Id)?.Include(u=>u.Posts).Include(u=>u.Comments).FirstOrDefault();
        if (userQuery is null)
        {
            return new GenericAppResult<AppUser>
            {
                Success = false,
                Data = null,
                Message = $"Cannot find any model with this id {request.Id}"
            };
        }

        return new GenericAppResult<AppUser>()
        {
            Success = true,
            OneData = userQuery,
            Message="Succesfull"
        };
    }
}

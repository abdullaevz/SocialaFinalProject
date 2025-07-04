using MediatR;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.UserQueries.GetUserById;

public class GetUserByIdQueryRequest:IRequest<GenericAppResult<AppUser>>
{
    public int Id { get; set; }

    public GetUserByIdQueryRequest(int ıd)
    {
        Id = ıd;
    }
}



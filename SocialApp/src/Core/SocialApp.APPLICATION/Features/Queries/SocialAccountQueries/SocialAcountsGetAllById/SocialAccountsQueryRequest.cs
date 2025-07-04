using MediatR;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.SocialAccountQueries.SocialAcountsGetAll;

public class SocialAccountsQueryRequest:IRequest<GenericAppResult<SocialAccount>>
{
    public int UserId { get; set; }

    public SocialAccountsQueryRequest(int Id)
    {
        UserId = Id;
    }
}

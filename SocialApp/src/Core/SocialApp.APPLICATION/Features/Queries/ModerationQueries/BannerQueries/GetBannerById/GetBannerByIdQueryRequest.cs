using MediatR;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.ModerationQueries.BannerQueries.GetBannerById;

public class GetBannerByIdQueryRequest : IRequest<GenericAppResult<Banner>>
{
    public int Id { get; set; }

    public GetBannerByIdQueryRequest(int ıd)
    {
        Id = ıd;
    }
}

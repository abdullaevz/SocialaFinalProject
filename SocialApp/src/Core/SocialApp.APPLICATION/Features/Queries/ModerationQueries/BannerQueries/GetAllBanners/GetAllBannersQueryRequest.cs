using MediatR;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.ModerationQueries.BannerQueries.GetAllBanners;

public class GetAllBannersQueryRequest : IRequest<GenericAppResult<Banner>>
{

}

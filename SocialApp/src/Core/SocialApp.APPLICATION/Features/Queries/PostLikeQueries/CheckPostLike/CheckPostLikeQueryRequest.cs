using MediatR;
using SocialApp.APPLICATION.ViewModels.PostLikeViewModels;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.PostLikeQueries.CheckPostLike;

public class CheckPostLikeQueryRequest : IRequest<GenericAppResult<LikesVM>>
{
    public int UserId { get; set; }

    public CheckPostLikeQueryRequest( int userId)
    {
        UserId = userId;
    }
}

using MediatR;
using SocialApp.APPLICATION.ViewModels.PostViewModels;
using SocialApp.DOMAIN.Enums;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.PostQueries.GetAllPosts;

public class GetAllPostsQueryRequest:IRequest<GenericAppResult<PostGetVM>>
{
    
}

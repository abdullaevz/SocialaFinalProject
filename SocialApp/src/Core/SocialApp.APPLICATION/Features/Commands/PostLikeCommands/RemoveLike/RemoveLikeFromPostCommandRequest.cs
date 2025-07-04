using MediatR;
using SocialApp.APPLICATION.ViewModels.PostLikeViewModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.PostLikeCommands.RemoveLike;

public class RemoveLikeFromPostCommandRequest : IRequest<AppResult>
{
    public RemoveLikeFromPostCommandRequest(LikeCreateVM likeCreateVM)
    {
        LikeCreateVM = likeCreateVM;
    }

    public LikeCreateVM LikeCreateVM { get; set; }
}

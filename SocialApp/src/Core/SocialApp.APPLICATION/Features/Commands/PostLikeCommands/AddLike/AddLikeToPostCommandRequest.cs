using MediatR;
using SocialApp.APPLICATION.ViewModels.PostLikeViewModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.PostLikeCommands.AddLike;

public class AddLikeToPostCommandRequest : IRequest<AppResult>
{
    public LikeCreateVM LikeCreateVM { get; set; }

    public AddLikeToPostCommandRequest(LikeCreateVM likeCreateVM)
    {
        LikeCreateVM = likeCreateVM;
    }
}

using MediatR;
using SocialApp.APPLICATION.ViewModels.PostViewModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.PostCommands.CreatePost;

public class CreatePostCommandRequest:IRequest<AppResult>
{
   public PostCreateVM PostCreateVM { get; set; }
    public CreatePostCommandRequest(PostCreateVM postCreateVM)
    {
        PostCreateVM = postCreateVM;
    }
}

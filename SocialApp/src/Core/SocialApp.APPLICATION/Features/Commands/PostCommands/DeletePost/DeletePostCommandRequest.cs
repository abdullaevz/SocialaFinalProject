using MediatR;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.PostCommands.DeletePost;

public class DeletePostCommandRequest:IRequest<AppResult>
{
    public int Id { get; set; }
    public DeletePostCommandRequest(int Id)
    {
        this.Id=Id;
    }
}

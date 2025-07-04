using MediatR;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.AppUserCommands.DeleteUser;

public class DeleteUserCommandRequest:IRequest<AppResult>
{
    public int Id { get; set; }

    public DeleteUserCommandRequest(int ıd)
    {
        Id = ıd;
    }
}

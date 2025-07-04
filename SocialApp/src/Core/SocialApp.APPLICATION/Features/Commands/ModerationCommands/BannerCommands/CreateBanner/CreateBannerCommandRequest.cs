using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.ModerationCommands.BannerCommands.CreateBanner;

public class CreateBannerCommandRequest:IRequest<AppResult>
{
    public IFormFile File { get; set; }

    public CreateBannerCommandRequest(IFormFile file)
    {
        File = file;
    }
}

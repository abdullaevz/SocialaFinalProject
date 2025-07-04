using MediatR;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using SocialApp.APPLICATION.ViewModels.BannerViewModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.ModerationCommands.BannerCommands.SelectBanner;

public class SelectBannerCommandRequest : IRequest<AppResult>
{
    public SelectBannerVM SelectBannerVM { get; set; }

    public SelectBannerCommandRequest(SelectBannerVM selectBannerVM)
    {
        SelectBannerVM = selectBannerVM;
    }
}

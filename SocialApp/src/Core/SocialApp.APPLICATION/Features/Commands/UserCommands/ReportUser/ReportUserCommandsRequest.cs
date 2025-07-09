using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.ViewModels.UserViewModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.UserCommands.ReportUser;

public class ReportUserCommandsRequest:IRequest<AppResult>
{
    public UserReportVM UserReportVM { get; set; }

    public ReportUserCommandsRequest(UserReportVM userReportVM)
    {
        UserReportVM = userReportVM;
    }
}

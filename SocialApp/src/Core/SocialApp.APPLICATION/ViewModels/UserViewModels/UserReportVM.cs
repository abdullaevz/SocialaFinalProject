using SocialApp.DOMAIN.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.UserViewModels;

public class UserReportVM
{
    public int ReporterId { get; set; }
    public int UserId { get; set; }
    public SecurityStatuses Status { get; set; }
}

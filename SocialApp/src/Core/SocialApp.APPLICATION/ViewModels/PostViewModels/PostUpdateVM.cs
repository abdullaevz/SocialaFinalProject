using SocialApp.DOMAIN.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.PostViewModels;

public class PostUpdateVM
{
    public int PostId { get; set; }
    public SecurityStatuses Status { get; set; }
    public string Content { get; set; }
    public SecurityStatuses SecurityStatuse { get; set; }
    public bool IsDeleted { get; set; }
}

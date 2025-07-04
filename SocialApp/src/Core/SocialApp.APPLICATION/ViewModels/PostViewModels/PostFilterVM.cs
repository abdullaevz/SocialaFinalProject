using SocialApp.DOMAIN.Enums;
using SocialApp.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.PostViewModels;

public class PostFilterVM
{
    public List<PostGetVM> Posts { get; set; }
    public bool ByNoFilter { get; set; }
    public bool ByIsDeleted { get; set; }
    public int ByUser { get; set; }
    public bool ByLikeCount { get; set; } 
    public bool ByCreationDate { get; set; }
    public DateTime? CreateDate { get; set; }
    public SecurityStatuses BySecurityStatuses { get; set; }
}

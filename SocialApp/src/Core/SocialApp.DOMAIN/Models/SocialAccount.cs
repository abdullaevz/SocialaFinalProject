using SocialApp.DOMAIN.Models.Common;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Models;

public class SocialAccount:BaseModel
{
    public string Name { get; set; }
    public string Url { get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; }
}

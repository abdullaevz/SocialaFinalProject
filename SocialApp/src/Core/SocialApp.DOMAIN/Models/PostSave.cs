using SocialApp.DOMAIN.Models.Common;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Models;

public class PostSave:BaseModel
{
    public int UserId { get; set; }
    public AppUser User { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
}

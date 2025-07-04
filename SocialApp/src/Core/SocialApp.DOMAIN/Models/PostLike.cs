using SocialApp.DOMAIN.Models.Common;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Models;

public class PostLike : BaseModel
{
    public int UserId { get; set; }
    public AppUser AppUser { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public bool IsLiked { get; set; }
}

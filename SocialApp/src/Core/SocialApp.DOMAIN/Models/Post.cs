using SocialApp.DOMAIN.Enums;
using SocialApp.DOMAIN.Models.Common;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Models;

public class Post : BaseModel
{
    public string Content { get; set; }
    public string MediaPath { get; set; }
    public SecurityStatuses SecurityStatus { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount{ get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<PostLike> PostLikes{ get; set; }
    public ICollection<PostSave> SavedPosts{ get; set; }
}

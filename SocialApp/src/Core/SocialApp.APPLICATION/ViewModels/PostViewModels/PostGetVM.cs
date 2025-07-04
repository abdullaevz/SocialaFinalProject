using SocialApp.APPLICATION.ViewModels.CommentViewModels;
using SocialApp.DOMAIN.Enums;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.PostViewModels;

public class PostGetVM
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string FileName { get; set; }
    public string MediaPath { get; set; }
    public bool IsDeleted { get; set; }
    public int LikeCount {  get; set; }
    public List<Comment> Comments { get; set; }
    public List<PostLike> PostLikes{ get; set; }
    public List<int> IsLikedBy { get; set; } = [];
    public AppUser User { get; set; }
    public DateTime CreationDate { get; set; }
    public SecurityStatuses SecurityStatus { get; set; }
}

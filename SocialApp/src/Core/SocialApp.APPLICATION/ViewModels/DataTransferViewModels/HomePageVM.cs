using SocialApp.APPLICATION.ViewModels.CommentViewModels;
using SocialApp.APPLICATION.ViewModels.PostLikeViewModels;
using SocialApp.APPLICATION.ViewModels.PostSaveViewModels;
using SocialApp.APPLICATION.ViewModels.PostViewModels;
using SocialApp.APPLICATION.ViewModels.UserFriendViewModels;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.DataTransferViewModels;

public class HomePageVM
{
    public PostCreateVM PostCreateModel { get; set; }
    public CreateCommentVM CreateCommentVM { get; set; }
    public AppUser LocalUser { get; set; }
    //public List<PostGetVM> Posts { get; set;}
    public List<PostGetVM> UserFriendPosts{ get; set;}
    public List<LikesVM> LikedPosts{ get; set;}
    public List<SaveGet> SavedPosts { get; set;}
    public List<UserFriendRequestGetVM> FriendRequests { get; set;}
    public List<UserFriendRequestGetVM> SuggestedUsers{ get; set;}
    
}

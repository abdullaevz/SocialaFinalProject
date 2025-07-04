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

namespace SocialApp.APPLICATION.ViewModels.UserViewModels;

public class UserProfileVM
{
    public AppUser User { get; set; }
    public AppUser LocalUser { get; set; }
    public string LocalUsername { get; set; }
    public bool AreFriends { get; set; }
    public List<SocialAccount> Accounts { get; set; }
    public List<PostGetVM> Posts { get; set; }
    public List<LikesVM> LikedPosts { get; set; }
    public List<SaveGet> SavedPosts{ get; set; }
    public List<int> BlockIds{ get; set; }
    public List<PostGetVM> OtherLikedPosts { get; set; }
    public List<CurrentFriendModel> CurrentFriends { get; set; }
    public List<PostGetVM> OtherSavedPosts { get; set; }
    public string ActiveBanner { get; set; }
}

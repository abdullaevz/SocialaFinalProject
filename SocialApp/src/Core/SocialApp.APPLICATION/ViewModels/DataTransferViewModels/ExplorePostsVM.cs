using SocialApp.APPLICATION.ViewModels.PostLikeViewModels;
using SocialApp.APPLICATION.ViewModels.PostSaveViewModels;
using SocialApp.APPLICATION.ViewModels.PostViewModels;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.DataTransferViewModels;

public class ExplorePostsVM
{
    public AppUser LocalUser { get; set; }
    public List<PostGetVM> AllPosts { get; set; }
    public List<LikesVM> Likes { get; set; }
    public List<SaveGet> Saves { get; set; }
    public List<string> FeaturedUsers { get; set; }
}

using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.UserFriendViewModels;

public class SuggestedFriends
{
    public List<AppUser> SuggestedList { get; set; }
}

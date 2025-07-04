using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.UserFriendViewModels;

public class UserFriendRequestGetVM
{
    public int UserId { get; set; }
    public int RequestId { get; set; }
    public string Fullname { get; set; }
    public string Username{ get; set; }
    public string ProfilePhoto { get; set; }
    public string DefaultProfilePhoto { get; set; }
}

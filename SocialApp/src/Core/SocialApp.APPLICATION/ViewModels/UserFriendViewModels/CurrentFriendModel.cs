using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.UserFriendViewModels;

public class CurrentFriendModel
{
    public int RequestId { get; set; }
    public int FriendId { get; set; }
    public string Username { get; set; }
    public string Fullname { get; set; }
    public string ProfilePhoto { get; set; }
    public string DefaultProfilePhoto { get; set; }
    public int PostCount { get; set; }
    public int FriendsCount { get; set; }
    public bool IsConfirmed { get; set; }
}

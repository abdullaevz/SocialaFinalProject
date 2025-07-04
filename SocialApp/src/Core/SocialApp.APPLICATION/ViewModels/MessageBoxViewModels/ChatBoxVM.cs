using SocialApp.APPLICATION.ViewModels.UserFriendViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.MessageBoxViewModels;

public class ChatBoxVM
{
    public int LocalUserId { get; set; }
    public Dictionary<string, string> OnlineUsersList { get; set; }
    public List<ChatUserVM> ChatBoxUsers { get; set; }

}

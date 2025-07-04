using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.MessageBoxViewModels;

public class ChatUserVM
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Fullname { get; set; }
    public string Initials { get; set; }
    public string LastMessage { get; set; }
}

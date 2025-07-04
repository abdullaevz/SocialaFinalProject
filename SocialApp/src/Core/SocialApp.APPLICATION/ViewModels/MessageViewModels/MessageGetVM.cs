using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.MessageViewModels;

public class MessageGetVM
{
    public int SenderId { get; set; }
    public int RecieverId { get; set; }
    public string Content { get; set; }
    public string SendDate { get; set; }
}

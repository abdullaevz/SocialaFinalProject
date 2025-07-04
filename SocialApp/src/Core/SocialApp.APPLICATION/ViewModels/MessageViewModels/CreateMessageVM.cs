using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.MessageViewModels;

public class CreateMessageVM
{
    [NotNull]
    public string Content { get; set; }
    public int SenderId { get; set; }
    public int RecieverId { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
}

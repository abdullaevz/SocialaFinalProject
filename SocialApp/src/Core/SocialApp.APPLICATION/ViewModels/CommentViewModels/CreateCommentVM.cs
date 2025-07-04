using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.CommentViewModels;

public class CreateCommentVM
{
    public int UserId { get; set; }
    public int PostId{ get; set; }
    [NotNull]
    [MaxLength(50,ErrorMessage ="Your content must be contains max 50 symbol")]
    public string Content{ get; set; }
}

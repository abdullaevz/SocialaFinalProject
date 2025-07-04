using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.UserViewModels;

public class UserPasswordUpdateVM
{
    public int UserId { get; set; }
    public string CurrentPasswordRaw { get; set; }
    public string NewPassword { get; set; }
    public string newPasswordConfirm { get; set; }
}

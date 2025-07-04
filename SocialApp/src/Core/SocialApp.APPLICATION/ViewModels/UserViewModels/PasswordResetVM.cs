using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.UserViewModels;

public class PasswordResetVM
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword{ get; set; }
}

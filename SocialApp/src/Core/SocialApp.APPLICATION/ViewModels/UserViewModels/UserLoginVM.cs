using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.UserViewModels;

public class UserLoginVM
{
    public string UsernameOrEmail{ get; set; }
    public string Password{ get; set; }
    public bool Remember { get; set; }
}

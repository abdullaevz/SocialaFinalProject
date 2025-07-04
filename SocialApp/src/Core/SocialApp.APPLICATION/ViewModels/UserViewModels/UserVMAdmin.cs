using SocialApp.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.UserViewModels;

public class UserVMAdmin
{
    public UserUpdateVM UpdateVM { get; set; }
    public List<Comment> Comments { get; set; }
    public List<Post> Posts { get; set; }

}

using SocialApp.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.SocialMediaViewModels;


public class SocialAccountVM
{
    public List<SocialAccount> socialAccountsList { get; set; }
    public string ErrorMessage { get; set; }
}



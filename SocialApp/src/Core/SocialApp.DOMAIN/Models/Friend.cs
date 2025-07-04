using SocialApp.DOMAIN.Models.Common;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Models;

public class Friend:BaseModel
{
    public int SenderUserId { get; set; }
    public int RecieverUserId{ get; set; }
    public AppUser SendedUser{ get; set; }
    public AppUser RecieverUser{ get; set; }
    public bool areFriends { get; set; }

}

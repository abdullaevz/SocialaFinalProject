using SocialApp.DOMAIN.Models.Common;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Models;

public class Message : BaseModel
{
    public int SenderId { get; set; }
    public AppUser Sender { get; set; }
    public int RecieverId { get; set; }
    public AppUser Reciever { get; set; }
    public string Content { get; set; }
}

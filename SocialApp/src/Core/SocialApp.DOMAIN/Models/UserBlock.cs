using SocialApp.DOMAIN.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Models;

public class UserBlock:BaseModel
{
    public int BlockedBy { get; set; }
    public int BlockedUser { get; set; }
}

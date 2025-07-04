using SocialApp.DOMAIN.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Models.Common;

public abstract class BaseModel
{
    public int Id { get; set; }
    public DateTime CreationDate{ get; set; }
    public bool IsDeleted { get; set; } 
    public UserRoles Role { get; set; }
}

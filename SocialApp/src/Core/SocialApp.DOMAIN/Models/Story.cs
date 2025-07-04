using SocialApp.DOMAIN.Enums;
using SocialApp.DOMAIN.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Models;

public class Story:BaseModel
{
    public int UserId { get; set; }
    public string ContentPath { get; set; }
    public SecurityStatuses SecurityStatus { get; set; }
}

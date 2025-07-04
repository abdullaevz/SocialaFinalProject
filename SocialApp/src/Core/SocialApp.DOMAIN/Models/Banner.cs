using SocialApp.DOMAIN.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Models;

public class Banner : BaseModel
{
    public string Url { get; set; }
    public bool IsActive { get; set; }
}

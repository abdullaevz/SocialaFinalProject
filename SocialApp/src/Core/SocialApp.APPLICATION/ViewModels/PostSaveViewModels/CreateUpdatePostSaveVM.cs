using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.PostSaveViewModels;

public class CreateUpdatePostSaveVM
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public bool IsSave { get; set; }
}

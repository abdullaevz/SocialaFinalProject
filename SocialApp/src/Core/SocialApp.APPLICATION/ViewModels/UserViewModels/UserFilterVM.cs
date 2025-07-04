using SocialApp.DOMAIN.Enums;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.UserViewModels;

public class UserFilterVM
{
    public List<AppUser> Users { get; set; }

    public bool ByNoFilter { get; set; }
    public bool ByIsDeleterd { get; set; }
    public bool ByEmailConfirmed { get; set; }
    public bool ByIsVerified { get; set; }
    public bool ByIsPrivite { get; set; }
    public UserRoles ByRole { get; set; }
    public DateTime ByCreationDate { get; set; }
    public SecurityStatuses BySecurityStatuses { get; set; }

}

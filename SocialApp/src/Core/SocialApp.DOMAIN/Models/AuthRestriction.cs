using SocialApp.DOMAIN.Models.Common;

namespace SocialApp.DOMAIN.Models;

public class AuthRestriction : BaseModel
{
    public bool LoginActionIsActive { get; set; }
    public bool RegisterActionIsActive { get; set; }
}

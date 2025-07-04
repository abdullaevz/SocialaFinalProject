using MediatR;
using SocialApp.APPLICATION.ViewModels.SocialMediaViewModels;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.SocialAccounts.CreateSocialAccount;

public class CreateSocialAccountCommandRequest : IRequest<AppResult>
{
    public int UserId { get; set; }


    public CreateSocialAccountCommandRequest(int userId)
    {
        UserId = userId;
    }

    //public AppUser User { get; set; }

    //public CreateSocialAccountCommandRequest(AppUser user)
    //{
    //    User = user;
    //}
}

using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Abstractions.Services;

public interface IEmailService
{
    Task<AppResult> ConfirmEmail(string userId, string token);
}

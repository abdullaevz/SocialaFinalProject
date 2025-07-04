using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Abstractions.Services;

public interface IStartupService
{
    Task CreateAdminAndRoles();
    Task CreateDefaultBanner();
    Task CreateDefaultAuthRestric();
}

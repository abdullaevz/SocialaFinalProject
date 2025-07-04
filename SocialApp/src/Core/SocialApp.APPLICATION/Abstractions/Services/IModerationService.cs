using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Abstractions.Services;

public interface IModerationService
{
    Task<string> SendPrompt(string content="Is this sentence safe?");
}

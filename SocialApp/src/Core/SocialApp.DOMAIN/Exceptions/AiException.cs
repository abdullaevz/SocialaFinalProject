using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Exceptions;

public class AiException:Exception
{
    public AiException():this("Default error:Something went wrong your ai prompt.")
    {
        
    }
    public AiException(string message):base(message)
    {
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Exceptions;

public class PostException:Exception
{
    public PostException():this("Default post exception:Something went wrong.")
    {
        
    }
    public PostException(string message):base(message) 
    {
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Exceptions;

public class MessageException:Exception
{
    public MessageException() : this("Default error message,Something went wrong about message model")
    {
        
    }

    public MessageException(string message):base(message) 
    {
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Exceptions;

public class FriendException:Exception
{
    public FriendException():this("Default error something went wrong about Friend model")
    {
        
    }

    public FriendException(string message):base(message) 
    {
        
    }
}

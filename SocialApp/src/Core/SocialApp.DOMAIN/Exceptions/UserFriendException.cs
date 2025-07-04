using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Exceptions;

public class UserFriendException:Exception
{
    public UserFriendException():this("Default error,something went wrong about UserFriend model")
    {
        
    }
    public UserFriendException(string message):base(message) 
    {

    }
}

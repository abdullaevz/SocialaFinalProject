using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Exceptions;

public class UserException :Exception
{
    public UserException():this("Default user exception.Something went wrong.")
    {
        
    }
    public UserException(string message):base(message)
    {
        
    }
}

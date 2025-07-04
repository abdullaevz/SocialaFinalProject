using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Exceptions;

public class CommentException:Exception
{
    public CommentException():this("Default message something went wrong about Comment model")
    {
        
    }

    public CommentException(string message):base(message) 
    {
        
    }
}

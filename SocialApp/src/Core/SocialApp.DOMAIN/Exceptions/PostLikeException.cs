using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Exceptions
{
    public class PostLikeException:Exception
    {
        public PostLikeException() : this("Default error:Something went wrong your ai prompt.")
        {

        }
        public PostLikeException(string message) : base(message)
        {

        }
    }
}

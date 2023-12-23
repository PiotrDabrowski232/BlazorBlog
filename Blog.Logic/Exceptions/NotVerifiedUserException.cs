using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Exceptions
{
    public class NotVerifiedUserException:Exception
    {
        public NotVerifiedUserException(string message) : base(message) { }
    }
}

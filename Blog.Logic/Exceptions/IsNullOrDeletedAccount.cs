using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Exceptions
{
    public class IsNullOrDeletedAccount : Exception
    {
        public IsNullOrDeletedAccount(string message) : base(message)
        {
        }
    }
}

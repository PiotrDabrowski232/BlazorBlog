using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Extensions
{
    public static class IntExtensions
    {
        public static Guid ToGuid(this int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }
}

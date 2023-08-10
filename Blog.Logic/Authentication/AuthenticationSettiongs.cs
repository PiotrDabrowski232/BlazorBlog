using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Authentication
{
    public class AuthenticationSettiongs
    {
        public string JwtKey { get; set; }
        public int JwtExpired { get; set; }
        public string JwtIssuer { get; set; }
    }
}

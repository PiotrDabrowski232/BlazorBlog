using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Dto.UserDtos
{
    public class UserSession
    {
        public string Email { get; set; }
        public RoleDto Role { get; set; }
    }
}

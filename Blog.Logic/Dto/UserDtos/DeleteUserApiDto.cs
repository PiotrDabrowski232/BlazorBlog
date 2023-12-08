using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Dto.UserDtos
{
    public class DeleteUserApiDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}

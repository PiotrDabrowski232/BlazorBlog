using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Dto.UserDtos
{
    public class AdminUserManagementDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDay { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string NewPasswordForNormalUser { get; set; }

    }
}

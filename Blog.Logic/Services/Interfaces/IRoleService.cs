using Blog.Data.Models;
using Blog.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Services.Interfaces
{
    public interface IRoleService
    {
        public IEnumerable<RoleDto> GetAll();
        public RoleDto GetRole(Guid roleId);
    }
}

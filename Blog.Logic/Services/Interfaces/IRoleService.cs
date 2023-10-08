using Blog.Logic.Dto;

namespace Blog.Logic.Services.Interfaces
{
    public interface IRoleService
    {
        public IEnumerable<RoleDto> GetAll();
        public RoleDto GetRole(Guid roleId);
    }
}

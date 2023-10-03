using AutoMapper;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Dto;
using Blog.Logic.Services.Interfaces;

namespace Blog.Logic.Services
{
    public class RoleService : IRoleService
    {

        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }


        public IEnumerable<RoleDto> GetAll()
        {
            var roles = _roleRepository.GetAll();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public RoleDto GetRole(Guid roleId)
        {
            return _mapper.Map<RoleDto>(_roleRepository.Get(roleId));
        }
    }
}

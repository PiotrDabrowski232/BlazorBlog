using Blog.Logic.Dto;
using Blog.Logic.Extensions;
using Blog.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [Route("/Roles/")]
        [HttpGet]
        public IEnumerable<RoleDto> Get()
        {
            var result = _roleService.GetAll();
            return result;
        }


        [Route("/Role/")]
        [HttpGet]
        public RoleDto Get([FromQuery]int id)
        {
            var result = _roleService.GetRole(id.ToGuid());
            return result;
        }
    }
}

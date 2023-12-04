using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Models.Enums;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Dto;
using Blog.Logic.Dto.UserDtos;
using Blog.Logic.Extensions;
using Blog.Logic.Services;
using Blog.Logic.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UnitTests.ServicesTests
{
    public class RoleServiceTest
    {
        private readonly Mock<IRoleRepository> _roleRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly IRoleService _roleService;

        public RoleServiceTest()
        {
            _mapper = new Mock<IMapper>();
            _roleRepository = new Mock<IRoleRepository>();
            _roleService = new RoleService(_roleRepository.Object, _mapper.Object);
        }

        [Fact]
        public void GetAll_Returned_CollectionOfRoleDto()
        {
            //Arrange
            var roles = new List<Roles>()
            {
                new Roles(){RoleId =1.ToGuid(), Name = UserRoles.BasicUser.ToString() },
                new Roles(){RoleId =2.ToGuid(), Name = UserRoles.Admin.ToString() },
                new Roles(){RoleId =3.ToGuid(), Name = UserRoles.SuperUser.ToString() }
            };

            _roleRepository.Setup(r => r.GetAll()).Returns(roles);

            _mapper.Setup(m => m.Map<IEnumerable<RoleDto>>(It.IsAny<IList<Roles>>())).Returns((IEnumerable<Roles> source) =>
            source.Select(role => new RoleDto
            {
                Id = role.RoleId,
                Name = role.Name,
            }));
            

            //Act

            var result = _roleService.GetAll();


            //Assert

            Assert.Equal(roles.Count,result.Count());
            _roleRepository.Verify(r => r.GetAll(), Times.Once);
        }
    }
}

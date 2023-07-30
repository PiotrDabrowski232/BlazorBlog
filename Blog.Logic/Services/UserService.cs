using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Dto;
using Blog.Logic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository= userRepository;
            _mapper= mapper;
        }

        public void Add(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            user.Id = Guid.NewGuid();
            _userRepository.Add(user);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Edit(UserDto user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserDto GetUser()
        {
            throw new NotImplementedException();
        }
    }
}

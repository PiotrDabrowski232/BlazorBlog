using Blog.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Blog.Logic.Dto.UserDtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public Guid RoleId { get; set; }
    }
}

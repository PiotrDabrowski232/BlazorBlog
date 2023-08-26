using Blog.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required] public string UserName { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }
        [Required] public Gender Gender { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public Guid RoleId { get; set; }
        public virtual Roles Role { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
    }
}

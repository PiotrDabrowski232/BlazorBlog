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
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime? LastLogginDate { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeleteDay { get; set; }

        public bool IsVerified { get; set; }
        public string? VerificationCode { get; set; }

        public Guid RoleId { get; set; }
        public virtual Roles Role { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
    }
}

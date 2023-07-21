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

        [Required(ErrorMessage ="Username Is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname Is Required")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
    }
}

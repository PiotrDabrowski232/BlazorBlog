using Blog.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Blog.Logic.Dto
{
    public class UserDto
    {
        [Required(ErrorMessage = "Specify Your User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Specify Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Specify Your Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Specify Your Email")]
        public string Email { get; set; }
        [Required] public Gender Gender { get; set; }

        [Required(ErrorMessage = "Specify Your Password")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmedPassword { get; set; }

        [Required(ErrorMessage = "Specify Your Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Specify Your City")]
        public string City { get; set; }
    }
}

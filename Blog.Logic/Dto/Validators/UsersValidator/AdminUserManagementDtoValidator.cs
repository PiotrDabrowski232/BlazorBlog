using Blog.Logic.Dto.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Dto.Validators.UserValidator
{
    public class AdminUserManagementDtoValidator : AbstractValidator<AdminUserManagementDto>
    {
        public AdminUserManagementDtoValidator()
        {
            RuleFor(u => u.NewPasswordForUser)
                .NotEmpty().WithMessage("Fill Password Input")
                .MinimumLength(8).WithMessage("Password should be longer than 8 characters")
                .Matches("[A-Z]").WithMessage("Password should contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password should contain at least one lowercase letter")
                .Matches("[!@#$%^&*()_+{}|:;<>,.?~]").WithMessage("Password should contain at least one special character");
        }
    }
}

using Blog.Logic.Dto.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Dto.Validators.UserValidator
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(l => l.Email)
                .EmailAddress()
                .WithMessage("Invalid Type");

            RuleFor(l => l.Password)
                .MinimumLength(8)
                .WithMessage("Password is too short");
        }
    }
}

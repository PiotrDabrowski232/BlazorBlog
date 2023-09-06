﻿using Blog.Data.Data;
using Blog.Logic.Dto.UserDtos;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Logic.Dto.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator(BlogDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Fill Name Input");

            RuleFor(x => x.UserName)
                .Custom((value, context) =>
                {
                    var usedName = dbContext.users.Any(u => u.UserName == value);

                    if (usedName)
                        context.AddFailure("UserName", "This UserName is taken");
                })
                .NotEmpty()
                .WithMessage("Fill UserName Input");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Fill Surname Input");

            RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Fill Password Input")
            .Unless(value =>
                !string.IsNullOrEmpty(value.Password) &&
                value.Password.Any(char.IsUpper) &&
                value.Password.Any(char.IsLower) &&
                value.Password.Any(c => !char.IsLetterOrDigit(c)) &&
                value.Password.Length>=8)
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one special character, and 8 characters");

            RuleFor(x => x.ConfirmedPassword)
                .Equal(e => e.Password)
                .WithMessage("Both passwords should be the same");

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("Fill City Input");

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Fill Country Input");

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var usedEmail = dbContext.users.Any(u => u.Email == value);

                    if (usedEmail)
                        context.AddFailure("Email", "This email is taken");
                })
                .NotEmpty()
                .WithMessage("Fill Email Input")
                .EmailAddress()
                .WithMessage("Invalid email format. Please enter a valid email address.");
        }
    }
}

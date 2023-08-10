using Blog.Data.Data;
using FluentValidation;

namespace Blog.Logic.Dto.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator(BlogDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.UserName)
                .NotEmpty();

            RuleFor(x => x.Surname)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();

            RuleFor(x => x.ConfirmedPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var usedEmail = dbContext.users.Any(u => u.Email == value);

                    if (usedEmail)
                        context.AddFailure("Email", "This email is taken");
                });
        }
    }
}

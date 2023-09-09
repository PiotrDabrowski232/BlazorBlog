using Blog.Logic.Dto.PostDtos;
using FluentValidation;

namespace Blog.Logic.Dto.Validators.PostsValidator
{
    public class PostDtoValidator : AbstractValidator<PostDto>
    {
        public PostDtoValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("You can't add Post without title")
                .MinimumLength(2).WithMessage("Title should be longer than 2 characters")
                .MaximumLength(30).WithMessage("Title should be no longer than 30 characters");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("You can't add Post without description")
                .MinimumLength(50).WithMessage("Description should be longer than 50 characters")
                .MaximumLength(250).WithMessage("Description should be no longer than 250 characters");
        }
    }
}

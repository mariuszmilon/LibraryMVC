using FluentValidation;

namespace LibraryMVC.Models.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(b => b.Email)
                .NotEmpty()
                .WithMessage("Address email is required.");

            RuleFor(b => b.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}

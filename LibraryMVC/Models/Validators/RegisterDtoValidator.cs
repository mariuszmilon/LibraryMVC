using FluentValidation;

namespace LibraryMVC.Models.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(b => b.FirstName)
               .NotEmpty()
               .WithMessage("First name is required.");

            RuleFor(b => b.LastName)
               .NotEmpty()
               .WithMessage("Last name is required.");

            RuleFor(a => a.Email)
               .NotEmpty()
               .WithMessage("Email address is required.");

            RuleFor(a => a.Password)
               .NotEmpty()
               .WithMessage("Password is required.");

            RuleFor(a => a.ConfirmPassword)
               .NotEmpty()
               .WithMessage("Comfirmed password is required.");

            RuleFor(a => a.ConfirmPassword)
                .Equal(p => p.Password)
                .WithMessage("The passwords are not the same.");

            RuleFor(a => a.Password)
                .Equal(p => p.ConfirmPassword)
                .WithMessage("The passwords are not the same.");
        }
    }
}

using FluentValidation;

namespace LibraryMVC.Models.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(b => b.UserName)
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

            RuleFor(a => a.Street)
                .NotEmpty()
                .WithMessage("Street is required.");

            RuleFor(a => a.City)
                .NotEmpty()
                .WithMessage("City is required.");

            RuleFor(a => a.PostalCode)
                .NotEmpty()
                .WithMessage("Zip code is required.");

            RuleFor(a => a.StreetNumber)
                .NotEmpty()
                .WithMessage("House number is required.");

            RuleFor(a => a.StreetNumber)
                .GreaterThan(0)
                .WithMessage("House number must be greater than 0.");
        }
    }
}

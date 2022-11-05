using FluentValidation;

namespace LibraryMVC.Models.Validators
{
    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        private string[] allowedRoleType = new string[] { "User", "Admin", "Employee" };
        public RoleDtoValidator()
        {
            RuleFor(r => r.Role)
                .Custom((value, context) =>
                {
                    if (!allowedRoleType.Contains(value))
                        context.AddFailure("Type", "Incorrect role type.");
                });
        }
    }
}

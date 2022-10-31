using FluentValidation;

namespace LibraryMVC.Models.Validators
{
    public class AddBookDtoValidator : AbstractValidator<AddBookDto>
    {
        public AddBookDtoValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty()
                .WithMessage("Title is required.");

            RuleFor(b => b.Author)
                .NotEmpty()
                .WithMessage("Author is required.");

            RuleFor(b => b.Publisher)
                .NotEmpty()
                .WithMessage("Publisher is required.");

            RuleFor(b => b.NumberOfPages)
                .NotEmpty()
                .WithMessage("Number of pages is required.");

            RuleFor(b => b.NumberOfPages)
                .GreaterThan(1)
                .WithMessage("Number of pages must be greather than 1.");

            RuleFor(b => b.DateOfPublication)
                .NotEmpty()
                .WithMessage("Date of publication is required.");

            RuleFor(b => b.IDNumber)
                .NotEmpty()
                .WithMessage("ID Number is required.");

            RuleFor(b => b.File)
                .NotNull()
                .WithMessage("Picture is required.");

            RuleFor(b => b.NumberOfPiecesAvailable)
                .NotEmpty()
                .WithMessage("Number of pieces available is required.");

            RuleFor(b => b.NumberOfPiecesAvailable)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Number of pieces available nust be greather or equal to 0.");

            RuleFor(b => b.CategoryId)
                .NotEmpty()
                .WithMessage("Category is required.");
        }
    }
}

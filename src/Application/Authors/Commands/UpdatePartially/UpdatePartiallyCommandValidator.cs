using System.Linq;
using FluentValidation;

namespace Cemiyet.Application.Authors.Commands.UpdatePartially
{
    public class UpdatePartiallyCommandValidator : AbstractValidator<UpdatePartiallyCommand>
    {
        public UpdatePartiallyCommandValidator()
        {
            RuleFor(upc => upc.Id).NotNull();

            RuleFor(upc => upc.Name)
                .NotEmpty().When(upc => string.IsNullOrEmpty(upc.Surname) && string.IsNullOrEmpty(upc.Bio));

            RuleFor(upc => upc.Name)
                .Must(ShouldNotContainDigits).WithMessage("Name alanı sayısal karakter içermemeli.")
                .When(upc => !string.IsNullOrEmpty(upc.Name));

            RuleFor(upc => upc.Name).MaximumLength(25);

            RuleFor(upc => upc.Surname)
                .NotEmpty().When(upc => string.IsNullOrEmpty(upc.Name) && string.IsNullOrEmpty(upc.Bio));

            RuleFor(upc => upc.Surname)
                .Must(ShouldNotContainDigits).WithMessage("Surname alanı sayısal karakter içermemeli.")
                .When(upc => !string.IsNullOrEmpty(upc.Surname));

            RuleFor(upc => upc.Surname).MaximumLength(25);

            RuleFor(upc => upc.Bio)
                .NotEmpty().When(upc => string.IsNullOrEmpty(upc.Name) && string.IsNullOrEmpty(upc.Surname));
            RuleFor(upc => upc.Bio).MaximumLength(2000);
        }

        private bool ShouldNotContainDigits(string s)
        {
            return !s.Any(char.IsDigit);
        }
    }
}

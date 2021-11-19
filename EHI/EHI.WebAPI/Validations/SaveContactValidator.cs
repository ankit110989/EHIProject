using EHI.Core.Models;
using FluentValidation;

namespace EHI.WebApi.Validations
{
    public class SaveContactValidator : AbstractValidator<SaveContactViewModel>
    {
        public SaveContactValidator()
        {
            RuleFor(m => m.FirstName).NotEmpty().MaximumLength(50);

            RuleFor(m => m.LastName).NotEmpty().MaximumLength(50);

            RuleFor(m => m.Email).NotEmpty().EmailAddress().MaximumLength(50);

            RuleFor(m => m.Phone).NotEmpty().MaximumLength(20);
        }
    }
}

using FluentValidation;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.Shared.Validators
{
    public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateDto>
    {
        public CustomerUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
        }
    }
}

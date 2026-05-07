using FluentValidation;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.Shared.Validators
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleForEach(x => x.ImageUrls).NotEmpty();
        }
    }
}

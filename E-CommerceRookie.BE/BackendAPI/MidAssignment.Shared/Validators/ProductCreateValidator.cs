using FluentValidation;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.Shared.Validators
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(200).WithMessage("Product name must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category ID is required.");

            RuleForEach(x => x.ImageUrls).ChildRules(image =>
            {
                image.RuleFor(i => i.Length)
                    .LessThanOrEqualTo(2 * 1024 * 1024)
                    .WithMessage("Image size must not exceed 2MB.");

                image.RuleFor(i => i.ContentType)
                    .Must(x => x != null && (x.Equals("image/jpeg") || x.Equals("image/png") || x.Equals("image/gif")))
                    .WithMessage("Only JPG, PNG, or GIF formats are allowed.");
            });
        }
    }
}

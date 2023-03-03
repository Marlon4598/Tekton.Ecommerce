using FluentValidation;
using Tekton.Ecommerce.Application.DTO;

namespace Tekton.Ecommerce.Application.Validator
{
    public class ProductDtoValidator : AbstractValidator<ProductsDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(u => u.Name).NotNull().NotEmpty();
            RuleFor(u => u.Description).NotNull().NotEmpty();
            RuleFor(u => u.Name).MaximumLength(80);
            RuleFor(u => u.Description).MaximumLength(150);
            RuleFor(u => u.Stock).GreaterThanOrEqualTo(u => 0);
            RuleFor(u => u.Price).GreaterThanOrEqualTo(u => 0);
        }

    }
}
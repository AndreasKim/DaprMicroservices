using FluentValidation;
using Services.ProductsService.Application.Commands;

namespace Services.ProductsService.Application.Validation
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(v => v.Name).NotEmpty();
        }
    }
}

using FluentValidation;
using Services.ProductsService.Application.Commands;

namespace Services.ProductsService.Application.Validation
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(v => v.Id).GreaterThan(0);
            RuleFor(v => v.Name).NotEmpty();
        }
    }
}

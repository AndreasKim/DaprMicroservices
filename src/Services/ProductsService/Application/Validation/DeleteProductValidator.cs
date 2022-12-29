using FluentValidation;
using Services.ProductsService.Application.Commands;

namespace Services.ProductsService.Application.Validation
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(v => v.Id).GreaterThan(0);
        }
    }
}

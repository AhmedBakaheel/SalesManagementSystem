using FluentValidation;
using SalesManagementSystem.Application.DTOs.Products;

namespace SalesManagementSystem.Application.Features.Products.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} لا يمكن أن يكون فارغًا.")
                .MaximumLength(100).WithMessage("{PropertyName} لا يجب أن يتجاوز 100 حرف.");

            RuleFor(p => p.CurrentPrice)
                .GreaterThan(0).WithMessage("{PropertyName} يجب أن يكون أكبر من الصفر.");

            RuleFor(p => p.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} لا يمكن أن يكون سالبًا.");

            RuleFor(p => p.Description)
                .MaximumLength(500).When(p => p.Description != null)
                .WithMessage("{PropertyName} يجب ألا يتجاوز 500 حرف.");
        }
    }
}
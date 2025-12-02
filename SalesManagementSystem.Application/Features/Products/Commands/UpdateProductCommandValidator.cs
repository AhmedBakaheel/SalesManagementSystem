using FluentValidation;

namespace SalesManagementSystem.Application.Features.Products.Commands
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            // 1. التحقق من وجود ID
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} مطلوب لتحديد المنتج.");

            // 2. التحقق من الخصائص الموروثة
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} لا يمكن أن يكون فارغًا.")
                .MaximumLength(100).WithMessage("{PropertyName} لا يجب أن يتجاوز 100 حرف.");

            RuleFor(p => p.CurrentPrice)
                .GreaterThan(0).WithMessage("{PropertyName} يجب أن يكون أكبر من الصفر.");

            RuleFor(p => p.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} لا يمكن أن يكون سالبًا.");
        }
    }
}
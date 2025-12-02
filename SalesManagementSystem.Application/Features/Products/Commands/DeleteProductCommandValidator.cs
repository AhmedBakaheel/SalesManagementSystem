using FluentValidation;

namespace SalesManagementSystem.Application.Features.Products.Commands
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} مطلوب لتحديد المنتج المراد حذفه.")
                .GreaterThan(0).WithMessage("{PropertyName} يجب أن يكون أكبر من الصفر.");
        }
    }
}
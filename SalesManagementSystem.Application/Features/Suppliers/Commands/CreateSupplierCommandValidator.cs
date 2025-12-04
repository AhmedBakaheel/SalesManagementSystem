using FluentValidation;
using SalesManagementSystem.Application.Features.Suppliers.Commands;

namespace SalesManagementSystem.Application.Features.Suppliers.Commands
{
    public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("اسم المورد مطلوب.")
                .MaximumLength(150).WithMessage("الاسم لا يجب أن يتجاوز 150 حرفاً.");

            RuleFor(s => s.Phone)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب.");
        }
    }
}
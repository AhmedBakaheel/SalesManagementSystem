using FluentValidation;

namespace SalesManagementSystem.Application.Features.Suppliers.Commands
{
    public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierCommandValidator()
        {
            RuleFor(s => s.Id)
                .GreaterThan(0).WithMessage("معرّف المورد مطلوب للتحديث.");

            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("اسم المورد مطلوب.");

            RuleFor(s => s.Phone)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب.");
        }
    }
}
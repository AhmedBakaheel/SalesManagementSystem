using FluentValidation;
using SalesManagementSystem.Application.Features.SupplierPayments.Commands;

public class CreateSupplierPaymentCommandValidator : AbstractValidator<CreateSupplierPaymentCommand>
{
    public CreateSupplierPaymentCommandValidator()
    {
        RuleFor(p => p.SupplierId).GreaterThan(0).WithMessage("معرف المورد مطلوب.");
        RuleFor(p => p.Amount).GreaterThan(0).WithMessage("يجب أن يكون مبلغ السداد أكبر من الصفر.");
    }
}
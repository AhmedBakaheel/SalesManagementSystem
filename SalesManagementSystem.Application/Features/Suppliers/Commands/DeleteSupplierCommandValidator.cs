using FluentValidation;

namespace SalesManagementSystem.Application.Features.Suppliers.Commands
{
    public class DeleteSupplierCommandValidator : AbstractValidator<DeleteSupplierCommand>
    {
        public DeleteSupplierCommandValidator()
        {
            RuleFor(s => s.Id)
                .GreaterThan(0).WithMessage("معرّف المورد مطلوب للحذف.");
        }
    }
}
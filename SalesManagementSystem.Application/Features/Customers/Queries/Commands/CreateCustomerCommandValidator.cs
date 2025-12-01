using FluentValidation;
using SalesManagementSystem.Application.DTOs.Customers;

namespace SalesManagementSystem.Application.Features.Customers.Commands
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty.") 
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty.")
                .NotNull();
            RuleFor(p => p.CreditLimit)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be zero or greater.");
        }
    }
}
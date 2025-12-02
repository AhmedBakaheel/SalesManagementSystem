using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Payments.Commands
{
    using FluentValidation;

    namespace SalesManagementSystem.Application.Features.Payments.Commands
    {
        public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
        {
            public CreatePaymentCommandValidator()
            {
                RuleFor(p => p.CustomerId)
                    .GreaterThan(0).WithMessage("معرف العميل مطلوب.");

                RuleFor(p => p.Amount)
                    .GreaterThan(0).WithMessage("يجب أن يكون مبلغ السداد أكبر من الصفر.");
            }
        }
    }
}

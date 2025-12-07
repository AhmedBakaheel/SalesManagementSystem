using MediatR;
using SalesManagementSystem.Application.DTOs.Customers;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.CustomerPayments.Commands
{
    public class CreateCustomerPaymentCommandHandler : IRequestHandler<CreateCustomerPaymentCommand, CustomerDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerPaymentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> Handle(CreateCustomerPaymentCommand request, CancellationToken cancellationToken)
        {
            
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                throw new Exception("العميل غير موجود.");
            }

            
            var payment = new Payment
            {
                CustomerId = request.CustomerId,
                Amount = request.Amount,
                PaymentDate = DateTime.Now,
                Notes = request.Notes
            };

            
            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.CompleteAsync(); 

            decimal remainingAmountToAllocate = request.Amount;
            decimal totalAllocated = 0;
            var invoicePayments = new List<InvoicePayment>();

            // جلب الفواتير المفتوحة للعميل
            var openInvoices = await _unitOfWork.Invoices.FindAsync(i =>
                i.CustomerId == request.CustomerId &&
                i.BalanceDue > 0);

            // فرز الفواتير حسب الأقدمية 
            var openInvoicesOrdered = openInvoices.OrderBy(i => i.InvoiceDate).ToList();

            foreach (var invoice in openInvoicesOrdered)
            {
                if (remainingAmountToAllocate <= 0)
                    break;

                decimal balanceDue = invoice.BalanceDue;
                decimal amountToAllocate = Math.Min(balanceDue, remainingAmountToAllocate);

                invoicePayments.Add(new InvoicePayment
                {
                    PaymentId = payment.Id,
                    InvoiceId = invoice.Id,
                    AllocatedAmount = amountToAllocate
                });

                invoice.BalanceDue -= amountToAllocate;
                _unitOfWork.Invoices.Update(invoice);

                remainingAmountToAllocate -= amountToAllocate;
                totalAllocated += amountToAllocate;
            }

            customer.Balance -= totalAllocated;

            if (remainingAmountToAllocate > 0)
            {
                customer.Balance -= remainingAmountToAllocate;
            }
            else
            {
                customer.Balance -= 0;
            }

            _unitOfWork.Customers.Update(customer);

            await _unitOfWork.InvoicePayments.AddRangeAsync(invoicePayments);
            await _unitOfWork.CompleteAsync();

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                //Phone = customer.Phone,
                Balance = customer.Balance
            };
        }
    }
}
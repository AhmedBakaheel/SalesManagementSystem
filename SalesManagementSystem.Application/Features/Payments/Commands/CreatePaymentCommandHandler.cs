using MediatR;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;
using System.Linq;

namespace SalesManagementSystem.Application.Features.Payments.Commands
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePaymentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);

            if (customer == null)
            {
                throw new Exception("Customer not found."); 
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

            var openInvoices = await _unitOfWork.Invoices.FindAsync(i =>
                i.CustomerId == request.CustomerId &&
                i.BalanceDue > 0);

            var openInvoicesOrdered = openInvoices.OrderBy(i => i.InvoiceDate);
            var invoicePayments = new List<InvoicePayment>();

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
            }

            customer.Balance -= (request.Amount - remainingAmountToAllocate);
            _unitOfWork.Customers.Update(customer);

            await _unitOfWork.InvoicePayments.AddRangeAsync(invoicePayments); 

            await _unitOfWork.CompleteAsync(); 

            return payment.Id;
        }
    }
}
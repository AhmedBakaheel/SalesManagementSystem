using MediatR;
using SalesManagementSystem.Application.DTOs.Invoices;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;
using SalesManagementSystem.Domain.Enums;

namespace SalesManagementSystem.Application.Features.Invoices.Commands
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, InvoiceCreateResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateInvoiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<InvoiceCreateResponse> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);
            if (customer == null || !customer.IsActive)
            {
                throw new Exception("Customer not found or is inactive.");
            }

            decimal totalAmount = 0;
            var invoiceDetails = new List<InvoiceDetail>();

            foreach (var detailDto in request.Details)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(detailDto.ProductId);

                if (product == null || product.StockQuantity < detailDto.Quantity)
                {
                    throw new Exception($"Product ID {detailDto.ProductId} is unavailable or quantity exceeded.");
                }

                decimal lineTotal = detailDto.Quantity * product.CurrentPrice;
                totalAmount += lineTotal;

                invoiceDetails.Add(new InvoiceDetail
                {
                    ProductId = detailDto.ProductId,
                    Quantity = detailDto.Quantity,
                    UnitPrice = product.CurrentPrice,
                    LineTotal = lineTotal
                });

                product.StockQuantity -= detailDto.Quantity;
                _unitOfWork.Products.Update(product);
            }

            if (request.PaymentType == PaymentType.Credit)
            {
                decimal newBalance = customer.Balance + totalAmount;
                if (newBalance > customer.CreditLimit)
                {
                    throw new Exception("Credit limit exceeded for this customer.");
                }

                customer.Balance = newBalance;
                _unitOfWork.Customers.Update(customer);
            }

            var invoice = new SaleInvoice
            {
                CustomerId = request.CustomerId,
                InvoiceDate = DateTime.Now,
                PaymentType = request.PaymentType,
                TotalAmount = totalAmount,
                TotalPaid = (request.PaymentType == PaymentType.Cash) ? totalAmount : 0,
                BalanceDue = (request.PaymentType == PaymentType.Cash) ? 0 : totalAmount,
                Details = invoiceDetails 
            };

            await _unitOfWork.Invoices.AddAsync(invoice);
            await _unitOfWork.CompleteAsync();

            return new InvoiceCreateResponse
            {
                InvoiceId = invoice.Id,
                TotalAmount = totalAmount,
                PaymentType = invoice.PaymentType.ToString()
            };
        }
    }
}
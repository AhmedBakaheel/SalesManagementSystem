using MediatR;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;
using SalesManagementSystem.Domain.Enums;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Sales.Commands
{
    public class CreateSaleInvoiceCommandHandler : IRequestHandler<CreateSaleInvoiceCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSaleInvoiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateSaleInvoiceCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                throw new Exception("العميل غير موجود.");
            }

            decimal totalInvoiceAmount = 0;
            var saleDetails = new List<InvoiceDetail>();


            foreach (var detailDto in request.Details)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(detailDto.ProductId);
                if (product == null)
                {
                    throw new Exception($"المنتج بالمعرّف {detailDto.ProductId} غير موجود.");
                }

                if (product.StockQuantity < detailDto.Quantity)
                {
                    throw new Exception($"المخزون غير كافٍ للمنتج {product.Name}. المتوفر: {product.StockQuantity}");
                }

                decimal priceAfterDiscount = detailDto.UnitPrice * (1 - detailDto.Discount / 100);
                decimal lineTotal = detailDto.Quantity * priceAfterDiscount;
                totalInvoiceAmount += lineTotal;

                saleDetails.Add(new InvoiceDetail
                {
                    ProductId = detailDto.ProductId,
                    Quantity = detailDto.Quantity,
                    UnitPrice = detailDto.UnitPrice,
                    Discount = detailDto.Discount,
                    LineTotal = lineTotal
                });
            }

            
            decimal totalReceived = 0;
            decimal balanceDue = totalInvoiceAmount;

            if (request.PaymentType == PaymentType.Cash)
            {
                totalReceived = totalInvoiceAmount;
                balanceDue = 0;
            }
            else if (request.CashReceived > 0)
            {
                totalReceived = Math.Min(request.CashReceived, totalInvoiceAmount);
                balanceDue = totalInvoiceAmount - totalReceived;
            }

            // فاتورة آجلة - تحديث رصيد العميل
            if (balanceDue > 0)
            {
                decimal potentialNewBalance = customer.Balance + balanceDue;

                // 💡 التحقق من سقف الدين 
                if (customer.CreditLimit > 0 && potentialNewBalance > customer.CreditLimit)
                {
                    throw new Exception($"تجاوز حد الائتمان للعميل {customer.Name}. الحد الأقصى: {customer.CreditLimit:N2}، الرصيد الجديد المتوقع: {potentialNewBalance:N2}");
                }

                
                customer.Balance += balanceDue;
                _unitOfWork.Customers.Update(customer);
            }

            
            //  تخفيض المخزون 
            foreach (var detail in saleDetails)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(detail.ProductId);
                product.StockQuantity -= detail.Quantity;
                _unitOfWork.ProductRepository.Update(product);
            }

            
            var saleInvoice = new SaleInvoice
            {
                CustomerId = request.CustomerId,
                InvoiceDate = DateTime.Now,
                TotalAmount = totalInvoiceAmount,
                TotalPaid = totalReceived,
                BalanceDue = balanceDue,
                Details = saleDetails
            };

            await _unitOfWork.Invoices.AddAsync(saleInvoice);

            await _unitOfWork.CompleteAsync();

            return saleInvoice.Id;
        }
    }
}
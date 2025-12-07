using MediatR;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;
using SalesManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Purchases.Commands
{
    public class CreatePurchaseInvoiceCommandHandler : IRequestHandler<CreatePurchaseInvoiceCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePurchaseInvoiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreatePurchaseInvoiceCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _unitOfWork.Suppliers.GetByIdAsync(request.SupplierId);
            if (supplier == null || !supplier.IsActive)
            {
                throw new Exception("المورد غير موجود أو غير نشط.");
            }

            decimal totalInvoiceAmount = 0;
            var purchaseDetails = new List<PurchaseInvoiceDetail>();

            foreach (var detailDto in request.Details)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(detailDto.ProductId);
                if (product == null)
                {
                    throw new Exception($"المنتج بالمعرّف {detailDto.ProductId} غير موجود.");
                }

                decimal lineTotal = detailDto.Quantity * detailDto.UnitCost;
                totalInvoiceAmount += lineTotal;

                
                product.StockQuantity += detailDto.Quantity;

                // product.CurrentCost = detailDto.UnitCost; 

                _unitOfWork.ProductRepository.Update(product);

                purchaseDetails.Add(new PurchaseInvoiceDetail
                {
                    ProductId = detailDto.ProductId,
                    Quantity = detailDto.Quantity,
                    UnitCost = detailDto.UnitCost,
                    LineTotal = lineTotal
                });
            }

            decimal totalPaid = 0;
            decimal balanceDue = totalInvoiceAmount;

            if (request.PaymentType == PaymentType.Cash)
            {
                totalPaid = totalInvoiceAmount;
                balanceDue = 0;
            }
            
            else if (request.CashPaid > 0)
            {
                totalPaid = Math.Min(request.CashPaid, totalInvoiceAmount);
                balanceDue = totalInvoiceAmount - totalPaid;
            }

            supplier.Balance += balanceDue;
            _unitOfWork.Suppliers.Update(supplier);

            var purchaseInvoice = new PurchaseInvoice
            {
                SupplierId = request.SupplierId,
                InvoiceDate = DateTime.Now,
                TotalAmount = totalInvoiceAmount,
                TotalPaid = totalPaid,
                BalanceDue = balanceDue,
                Details = purchaseDetails 
            };

            await _unitOfWork.PurchaseInvoices.AddAsync(purchaseInvoice);

            await _unitOfWork.CompleteAsync();

            return purchaseInvoice.Id;
        }
    }
}
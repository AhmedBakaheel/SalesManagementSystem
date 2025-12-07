using MediatR;
using SalesManagementSystem.Application.DTOs.Purchases;
using SalesManagementSystem.Domain.Enums; 

namespace SalesManagementSystem.Application.Features.Purchases.Commands
{
    public class CreatePurchaseInvoiceCommand : IRequest<int>
    {
        public int SupplierId { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal CashPaid { get; set; } = 0;
        public List<PurchaseDetailDto> Details { get; set; } = new List<PurchaseDetailDto>();
    }
}
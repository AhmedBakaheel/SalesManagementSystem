using MediatR;
using SalesManagementSystem.Application.DTOs.Sales;
using SalesManagementSystem.Domain.Enums;

namespace SalesManagementSystem.Application.Features.Sales.Commands
{
    public class CreateSaleInvoiceCommand : IRequest<int>
    {
        public int CustomerId { get; set; }
        public PaymentType PaymentType { get; set; } 
        public decimal CashReceived { get; set; } = 0;
        public List<SaleDetailDto> Details { get; set; } = new List<SaleDetailDto>();
    }
}
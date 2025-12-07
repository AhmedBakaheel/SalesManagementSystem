using MediatR;
using SalesManagementSystem.Application.DTOs.Suppliers; 
namespace SalesManagementSystem.Application.Features.SupplierPayments.Commands
{
    public class CreateSupplierPaymentCommand : IRequest<SupplierDto>
    {
        public int SupplierId { get; set; }
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
    }
}
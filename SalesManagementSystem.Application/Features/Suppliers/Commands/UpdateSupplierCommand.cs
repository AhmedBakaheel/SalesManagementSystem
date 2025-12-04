using MediatR;
using SalesManagementSystem.Application.DTOs.Suppliers;

namespace SalesManagementSystem.Application.Features.Suppliers.Commands
{
    public class UpdateSupplierCommand : IRequest<SupplierDto>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsActive { get; set; } 
    }
}
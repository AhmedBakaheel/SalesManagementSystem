using MediatR;
using SalesManagementSystem.Application.DTOs.Suppliers;

namespace SalesManagementSystem.Application.Features.Suppliers.Commands
{
    public class CreateSupplierCommand : CreateSupplierDto, IRequest<SupplierDto>
    {
    }
}
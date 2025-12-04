using MediatR;
using SalesManagementSystem.Application.DTOs.Suppliers;

namespace SalesManagementSystem.Application.Features.Suppliers.Queries
{
    public class GetSupplierByIdQuery : IRequest<SupplierDto?>
    {
        public int Id { get; set; }

        public GetSupplierByIdQuery(int id)
        {
            Id = id;
        }
    }
}
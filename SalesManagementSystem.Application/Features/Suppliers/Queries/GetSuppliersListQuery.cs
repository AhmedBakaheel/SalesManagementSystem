using MediatR;
using SalesManagementSystem.Application.DTOs.Suppliers;
using System.Collections.Generic;

namespace SalesManagementSystem.Application.Features.Suppliers.Queries
{
    public class GetSuppliersListQuery : IRequest<IEnumerable<SupplierDto>>
    {
        public string? SearchTerm { get; set; }
    }
}
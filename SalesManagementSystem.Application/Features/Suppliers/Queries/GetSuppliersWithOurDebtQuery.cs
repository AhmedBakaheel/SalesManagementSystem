using MediatR;
using SalesManagementSystem.Application.DTOs.Suppliers;
using System.Collections.Generic;

namespace SalesManagementSystem.Application.Features.Suppliers.Queries
{
    public class GetSuppliersWithOurDebtQuery : IRequest<IEnumerable<SupplierDto>>
    {
        
    }
}
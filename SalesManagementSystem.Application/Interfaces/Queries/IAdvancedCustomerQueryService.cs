using SalesManagementSystem.Application.DTOs.Common;
using SalesManagementSystem.Application.DTOs.Customers;
using SalesManagementSystem.Application.DTOs.Products;
using SalesManagementSystem.Application.Features.Products.Queries;

namespace SalesManagementSystem.Application.Interfaces.Queries
{
    public interface IAdvancedCustomerQueryService
    {
        Task<PagedResponse<CustomerDto>> GetPagedCustomersAsync(
            PaginationParams parameters,
            CancellationToken cancellationToken);
    }
}
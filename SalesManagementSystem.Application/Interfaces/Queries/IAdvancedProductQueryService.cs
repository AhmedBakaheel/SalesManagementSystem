using SalesManagementSystem.Application.DTOs.Common;
using SalesManagementSystem.Application.DTOs.Products;
using SalesManagementSystem.Application.Features.Products.Queries;

namespace SalesManagementSystem.Application.Interfaces.Queries
{
    public interface IAdvancedProductQueryService
    {
        Task<PagedResponse<ProductDto>> GetPagedProductsAsync(
            GetAllProductsQuery request, CancellationToken cancellationToken);
    }
}
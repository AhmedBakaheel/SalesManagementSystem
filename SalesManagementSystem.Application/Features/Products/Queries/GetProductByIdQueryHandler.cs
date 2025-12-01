using MediatR;
using SalesManagementSystem.Application.DTOs.Common;
using SalesManagementSystem.Application.DTOs.Products;
using SalesManagementSystem.Application.Interfaces.Queries;

namespace SalesManagementSystem.Application.Features.Products.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResponse<ProductDto>>
    {
        private readonly IAdvancedProductQueryService _queryService;

        public GetAllProductsQueryHandler(IAdvancedProductQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<PagedResponse<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _queryService.GetPagedProductsAsync(request, cancellationToken);
        }
    }
}
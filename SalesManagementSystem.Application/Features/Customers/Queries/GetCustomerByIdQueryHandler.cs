using MediatR;
using SalesManagementSystem.Application.DTOs.Common;
using SalesManagementSystem.Application.DTOs.Customers;
using SalesManagementSystem.Application.Interfaces.Queries; 

namespace SalesManagementSystem.Application.Features.Customers.Queries
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, PagedResponse<CustomerDto>>
    {
        private readonly IAdvancedCustomerQueryService _queryService;

        public GetAllCustomersQueryHandler(IAdvancedCustomerQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<PagedResponse<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _queryService.GetPagedCustomersAsync(request, cancellationToken);
        }
    }
}
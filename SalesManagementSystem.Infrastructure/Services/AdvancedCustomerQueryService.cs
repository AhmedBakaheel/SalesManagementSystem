using SalesManagementSystem.Application.DTOs.Common;
using SalesManagementSystem.Application.DTOs.Customers;
using SalesManagementSystem.Application.Interfaces.Queries;
using SalesManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SalesManagementSystem.Infrastructure.Services
{
    public class AdvancedCustomerQueryService : IAdvancedCustomerQueryService
    {
        private readonly ApplicationDbContext _dbContext;

        public AdvancedCustomerQueryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResponse<CustomerDto>> GetPagedCustomersAsync(
            PaginationParams parameters,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.Where(c =>
                    c.Name.Contains(parameters.SearchTerm) ||
                    c.Phone.Contains(parameters.SearchTerm)
                );
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var customers = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CreditLimit = c.CreditLimit,
                    Balance = c.Balance
                })
                .ToListAsync(cancellationToken);

            return new PagedResponse<CustomerDto>(
                customers,
                totalCount,
                parameters.PageNumber,
                parameters.PageSize
            );
        }
    }
}
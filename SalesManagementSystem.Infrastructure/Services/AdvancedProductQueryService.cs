using SalesManagementSystem.Application.DTOs.Common;
using SalesManagementSystem.Application.DTOs.Products;
using SalesManagementSystem.Application.Interfaces.Queries;
using SalesManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Application.Features.Products.Queries;
using System.Linq; // للتأكد من توفر LINQ

namespace SalesManagementSystem.Infrastructure.Services
{
    public class AdvancedProductQueryService : IAdvancedProductQueryService
    {
        private readonly ApplicationDbContext _dbContext;

        public AdvancedProductQueryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResponse<ProductDto>> GetPagedProductsAsync(
            GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(p =>
                    p.Name.Contains(request.SearchTerm) 
                    //||
                    //p.Description.Contains(request.SearchTerm)
                );
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var products = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    //CurrentPrice = p.UnitPrice,
                    //StockQuantity = p.InStock
                    CurrentPrice = p.CurrentPrice,
                    StockQuantity = p.StockQuantity

                })
                .ToListAsync(cancellationToken);

            return new PagedResponse<ProductDto>(
                products,
                totalCount,
                request.PageNumber,
                request.PageSize
            );
        }
    }
}
using MediatR;
using SalesManagementSystem.Application.DTOs.Common;
using SalesManagementSystem.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Products.Queries
{
    public class GetAllProductsQuery : PaginationParams, IRequest<PagedResponse<ProductDto>>
    {
    }
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}

using MediatR;
using SalesManagementSystem.Application.DTOs.Common;
using SalesManagementSystem.Application.DTOs.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Customers.Queries
{
    public class GetAllCustomersQuery : PaginationParams, IRequest<PagedResponse<CustomerDto>>
    {
    }
    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public int Id { get; set; } 
    }
}

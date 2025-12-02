using MediatR;
using SalesManagementSystem.Application.DTOs.Customers;
using System.Collections.Generic;

namespace SalesManagementSystem.Application.Features.Customers.Queries
{
    public class GetCustomersWithDebtQuery : IRequest<IEnumerable<CustomerDebtDto>>
    {
    }
}
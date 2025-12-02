using MediatR;
using SalesManagementSystem.Application.DTOs.Invoices;

namespace SalesManagementSystem.Application.Features.Invoices.Queries
{
    public class GetTotalSalesQuery : IRequest<TotalSalesDto>
    {
    }
}
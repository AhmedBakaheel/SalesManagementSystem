using MediatR;
using SalesManagementSystem.Application.DTOs.Customers; 

namespace SalesManagementSystem.Application.Features.CustomerPayments.Commands
{
    public class CreateCustomerPaymentCommand : IRequest<CustomerDto>
    {
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
    }
}
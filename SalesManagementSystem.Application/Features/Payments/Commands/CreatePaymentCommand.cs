using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Payments.Commands
{
    public class CreatePaymentCommand : IRequest<int>
    {
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
    }
}

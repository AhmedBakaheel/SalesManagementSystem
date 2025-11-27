using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.DTOs.Customers
{
    public class CreateCustomerCommand : IRequest<CustomerDto>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public decimal CreditLimit { get; set; } = 0;
    }
}

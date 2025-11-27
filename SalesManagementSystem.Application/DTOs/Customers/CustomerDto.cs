using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.DTOs.Customers
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Balance { get; set; }
    }
}

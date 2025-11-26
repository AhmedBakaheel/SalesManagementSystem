using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal CreditLimit { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<SaleInvoice> Invoices { get; set; } = new List<SaleInvoice>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public decimal Amount { get; set; } 
        public string Notes { get; set; } = string.Empty;
        public Customer Customer { get; set; } = null!; 
        public ICollection<InvoicePayment> Allocations { get; set; } = new List<InvoicePayment>();
    }
}

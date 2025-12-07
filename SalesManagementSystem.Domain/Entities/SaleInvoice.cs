using SalesManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Domain.Entities
{
    public class SaleInvoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public PaymentType PaymentType { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal BalanceDue { get; set; }

        // Navigation Properties
        public Customer Customer { get; set; } = null!; 
        public ICollection<InvoiceDetail> Details { get; set; } = new List<InvoiceDetail>();
        public ICollection<InvoicePayment> AllocatedPayments { get; set; } = new List<InvoicePayment>();
    }
}

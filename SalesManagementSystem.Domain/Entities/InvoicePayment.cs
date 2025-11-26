using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Domain.Entities
{
    public class InvoicePayment
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public decimal AllocatedAmount { get; set; }
        public Payment Payment { get; set; } = null!; 
        public SaleInvoice Invoice { get; set; } = null!;
    }
}

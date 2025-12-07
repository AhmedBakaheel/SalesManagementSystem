using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Domain.Entities
{
    public class PurchaseInvoice
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPaid { get; set; } 
        public decimal BalanceDue { get; set; }

        public Supplier Supplier { get; set; }
        public ICollection<PurchaseInvoiceDetail> Details { get; set; } = new List<PurchaseInvoiceDetail>();
    }
}
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Domain.Entities
{
    public class PurchaseInvoiceDetail
    {
        public int Id { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; } 
        public decimal LineTotal { get; set; }
        public PurchaseInvoice PurchaseInvoice { get; set; }
        public Product Product { get; set; }
    }
}
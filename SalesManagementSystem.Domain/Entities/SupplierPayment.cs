using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Domain.Entities
{
    public class SupplierPayment
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; } 
        public string? Notes { get; set; }
        public Supplier Supplier { get; set; }
    }
}
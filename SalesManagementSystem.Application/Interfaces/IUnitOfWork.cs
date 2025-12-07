using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<SaleInvoice> Invoices { get; }
        IRepository<Payment> Payments { get; }
        IRepository<InvoiceDetail> InvoiceDetails { get; }
        IRepository<InvoicePayment> InvoicePayments { get; }
        IRepository<Supplier> Suppliers { get; }
        IRepository<PurchaseInvoice> PurchaseInvoices { get; }
        IRepository<PurchaseInvoiceDetail> PurchaseInvoiceDetails { get; }
        IRepository<SupplierPayment> SupplierPayments { get; }
        Task<int> CompleteAsync();
    }
}
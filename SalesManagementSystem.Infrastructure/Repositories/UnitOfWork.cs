using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;
using SalesManagementSystem.Infrastructure.Data;

namespace SalesManagementSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Customer> Customers { get; private set; }
        public IRepository<Product> ProductRepository { get; private set; }
        public IRepository<SaleInvoice> Invoices { get; private set; }
        public IRepository<Payment> Payments { get; private set; }
        public IRepository<InvoiceDetail> InvoiceDetails { get; private set; }
        public IRepository<InvoicePayment> InvoicePayments { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Customers = new Repository<Customer>(_context);
            ProductRepository = new Repository<Product>(_context);
            Invoices = new Repository<SaleInvoice>(_context);
            Payments = new Repository<Payment>(_context);
            InvoiceDetails = new Repository<InvoiceDetail>(_context);
            InvoicePayments = new Repository<InvoicePayment>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
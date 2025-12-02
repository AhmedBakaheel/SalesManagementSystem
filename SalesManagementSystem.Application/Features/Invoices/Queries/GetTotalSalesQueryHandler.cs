using MediatR;
using SalesManagementSystem.Application.DTOs.Invoices;
using SalesManagementSystem.Application.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Invoices.Queries
{
    public class GetTotalSalesQueryHandler : IRequestHandler<GetTotalSalesQuery, TotalSalesDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTotalSalesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TotalSalesDto> Handle(GetTotalSalesQuery request, CancellationToken cancellationToken)
        {
            var allInvoices = await _unitOfWork.Invoices.GetAllAsync();

            decimal totalAmount = allInvoices.Sum(i => i.TotalAmount);
            int totalCount = allInvoices.Count();

            return new TotalSalesDto
            {
                TotalAmount = totalAmount,
                TotalInvoices = totalCount
            };
        }
    }
}
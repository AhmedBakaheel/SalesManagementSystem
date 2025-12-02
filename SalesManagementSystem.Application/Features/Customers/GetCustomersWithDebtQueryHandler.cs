using MediatR;
using SalesManagementSystem.Application.DTOs.Customers;
using SalesManagementSystem.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Customers.Queries
{
    public class GetCustomersWithDebtQueryHandler : IRequestHandler<GetCustomersWithDebtQuery, IEnumerable<CustomerDebtDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomersWithDebtQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomerDebtDto>> Handle(GetCustomersWithDebtQuery request, CancellationToken cancellationToken)
        {
            var debtCustomers = await _unitOfWork.Customers.FindAsync(c => c.Balance > 0);

            var result = debtCustomers
                .Select(c => new CustomerDebtDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Phone = c.Phone,
                    Balance = c.Balance
                })
                .OrderByDescending(d => d.Balance)
                .ToList();

            return result;
        }
    }
}
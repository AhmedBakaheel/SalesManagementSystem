using MediatR;
using SalesManagementSystem.Application.DTOs.Suppliers;
using SalesManagementSystem.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Suppliers.Queries
{
    public class GetSuppliersWithOurDebtQueryHandler : IRequestHandler<GetSuppliersWithOurDebtQuery, IEnumerable<SupplierDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSuppliersWithOurDebtQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SupplierDto>> Handle(GetSuppliersWithOurDebtQuery request, CancellationToken cancellationToken)
        {
            var suppliersWithDebt = await _unitOfWork.Suppliers.FindAsync(s =>
                s.IsActive && s.Balance > 0);

            var result = suppliersWithDebt
                .OrderByDescending(s => s.Balance)
                .Select(s => new SupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Phone = s.Phone,
                    Balance = s.Balance 
                })
                .ToList();

            return result;
        }
    }
}
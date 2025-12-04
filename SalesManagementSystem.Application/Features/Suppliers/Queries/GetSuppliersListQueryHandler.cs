using MediatR;
using SalesManagementSystem.Application.DTOs.Suppliers;
using SalesManagementSystem.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Suppliers.Queries
{
    public class GetSuppliersListQueryHandler : IRequestHandler<GetSuppliersListQuery, IEnumerable<SupplierDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSuppliersListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SupplierDto>> Handle(GetSuppliersListQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _unitOfWork.Suppliers.FindAsync(s => s.IsActive);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                string searchTermLower = request.SearchTerm.ToLower();

                suppliers = suppliers.Where(s =>
                    s.Name.ToLower().Contains(searchTermLower) ||
                    s.Phone.Contains(searchTermLower) ||
                    s.Address.ToLower().Contains(searchTermLower));
            }

            var result = suppliers
                .OrderBy(s => s.Name)
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
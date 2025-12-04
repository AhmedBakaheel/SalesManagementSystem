using MediatR;
using SalesManagementSystem.Application.DTOs.Suppliers;
using SalesManagementSystem.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Suppliers.Queries
{
    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, SupplierDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSupplierByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SupplierDto?> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _unitOfWork.Suppliers.GetByIdAsync(request.Id);

            if (supplier == null || !supplier.IsActive)
            {
                return null;
            }

            var supplierDto = new SupplierDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Phone = supplier.Phone,
                Balance = supplier.Balance
            };

            return supplierDto;
        }
    }
}
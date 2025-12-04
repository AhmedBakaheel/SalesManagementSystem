using MediatR;
using SalesManagementSystem.Application.DTOs.Suppliers;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Features.Suppliers.Commands
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, SupplierDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSupplierCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SupplierDto> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierEntity = new Supplier
            {
                Name = request.Name,
                Phone = request.Phone,
                Address = request.Address,
                Balance = 0, 
                IsActive = true
            };

            
            await _unitOfWork.Suppliers.AddAsync(supplierEntity);
            await _unitOfWork.CompleteAsync();

            
            var supplierDto = new SupplierDto
            {
                Id = supplierEntity.Id,
                Name = supplierEntity.Name,
                Phone = supplierEntity.Phone,
                Balance = supplierEntity.Balance
            };

            return supplierDto;
        }
    }
}
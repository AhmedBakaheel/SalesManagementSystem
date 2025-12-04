using MediatR;
using SalesManagementSystem.Application.DTOs.Suppliers;
using SalesManagementSystem.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Suppliers.Commands
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, SupplierDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSupplierCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SupplierDto> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierEntity = await _unitOfWork.Suppliers.GetByIdAsync(request.Id);

            if (supplierEntity == null)
            {
                throw new Exception($"لم يتم العثور على مورد بالمعرّف: {request.Id}");
            }

            supplierEntity.Name = request.Name;
            supplierEntity.Phone = request.Phone;
            supplierEntity.Address = request.Address;
            supplierEntity.IsActive = request.IsActive;

            _unitOfWork.Suppliers.Update(supplierEntity);
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
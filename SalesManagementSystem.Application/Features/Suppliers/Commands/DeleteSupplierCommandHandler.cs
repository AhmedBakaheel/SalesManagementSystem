using MediatR;
using SalesManagementSystem.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Suppliers.Commands
{
    
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSupplierCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierEntity = await _unitOfWork.Suppliers.GetByIdAsync(request.Id);

            if (supplierEntity == null)
            {
                throw new Exception($"لم يتم العثور على مورد بالمعرّف: {request.Id}");
            }

            if (supplierEntity.Balance > 0)
            {
                throw new Exception("لا يمكن حذف المورد لوجود رصيد مستحق عليه. يجب تصفية الرصيد أولاً.");
            }

            supplierEntity.IsActive = false;

            _unitOfWork.Suppliers.Update(supplierEntity);
            await _unitOfWork.CompleteAsync();

            return Unit.Value; 
        }
    }
}
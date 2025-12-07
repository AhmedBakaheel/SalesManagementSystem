using MediatR;
using SalesManagementSystem.Application.DTOs.Suppliers;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Features.SupplierPayments.Commands
{
    public class CreateSupplierPaymentCommandHandler : IRequestHandler<CreateSupplierPaymentCommand, SupplierDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSupplierPaymentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SupplierDto> Handle(CreateSupplierPaymentCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _unitOfWork.Suppliers.GetByIdAsync(request.SupplierId);
            if (supplier == null)
            {
                throw new Exception("المورد غير موجود.");
            }

            
            if (request.Amount > supplier.Balance)
            {
                
            }

            var payment = new SupplierPayment
            {
                SupplierId = request.SupplierId,
                Amount = request.Amount,
                PaymentDate = DateTime.Now,
                Notes = request.Notes
            };

            supplier.Balance -= request.Amount;

            _unitOfWork.Suppliers.Update(supplier);
            await _unitOfWork.SupplierPayments.AddAsync(payment); 

            await _unitOfWork.CompleteAsync();

            return new SupplierDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Phone = supplier.Phone,
                Balance = supplier.Balance 
            };
        }
    }
}
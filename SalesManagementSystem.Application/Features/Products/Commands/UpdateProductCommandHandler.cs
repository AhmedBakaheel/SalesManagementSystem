using MediatR;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Features.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

            if (productEntity == null)
            {
                return false;
            }

            productEntity.Name = request.Name;
            productEntity.Description = request.Description;
            productEntity.CurrentPrice = request.CurrentPrice; 
            productEntity.StockQuantity = request.StockQuantity;
            _unitOfWork.ProductRepository.Update(productEntity);

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
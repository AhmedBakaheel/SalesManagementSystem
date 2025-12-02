using MediatR;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Features.Products.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

            if (productEntity == null)
            {
                return false;
            }

            _unitOfWork.ProductRepository.Remove(productEntity);

            
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
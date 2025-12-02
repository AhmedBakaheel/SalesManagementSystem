using MediatR;
using SalesManagementSystem.Application.DTOs.Products;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = new Product
            {
                Name = request.Name,
                Description = request.Description,
                CurrentPrice = request.CurrentPrice, 
                StockQuantity = request.StockQuantity 
            };

            await _unitOfWork.ProductRepository.AddAsync(productEntity);
            await _unitOfWork.CompleteAsync();

            var productDto = new ProductDto
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                CurrentPrice = productEntity.CurrentPrice,
                StockQuantity = productEntity.StockQuantity
            };

            return productDto;
        }
    }
}
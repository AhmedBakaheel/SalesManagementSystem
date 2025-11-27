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
            var product = new Product
            {
                Name = request.Name,
                CurrentPrice = request.CurrentPrice,
                StockQuantity = request.InitialStockQuantity
            };

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                CurrentPrice = product.CurrentPrice,
                StockQuantity = product.StockQuantity
            };
        }
    }
}
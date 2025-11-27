using MediatR;
using SalesManagementSystem.Application.DTOs.Products;
using SalesManagementSystem.Application.Interfaces;

namespace SalesManagementSystem.Application.Features.Products.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

            if (product == null)
            {
                return null!; 
            }

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
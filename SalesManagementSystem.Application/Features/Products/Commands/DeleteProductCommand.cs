using MediatR;

namespace SalesManagementSystem.Application.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
using MediatR;

namespace SalesManagementSystem.Application.Features.Suppliers.Commands
{
    public class DeleteSupplierCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteSupplierCommand(int id)
        {
            Id = id;
        }
    }
}
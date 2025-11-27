using MediatR;
using SalesManagementSystem.Application.DTOs.Customers;
using SalesManagementSystem.Application.Interfaces;

namespace SalesManagementSystem.Application.Features.Customers.Queries
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.Id);

            if (customer == null)
            {
                throw new Exception($"Customer with ID {request.Id} not found.");
            }

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                //Phone = customer.Phone,
                CreditLimit = customer.CreditLimit,
                Balance = customer.Balance
            };
        }
    }
}
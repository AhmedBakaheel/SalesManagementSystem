using MediatR;
using SalesManagementSystem.Application.DTOs.Customers;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Application.Features.Customers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Phone = request.Phone,
                CreditLimit = request.CreditLimit,
                Balance = 0.00m,
                IsActive = true
            };

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                CreditLimit = customer.CreditLimit,
                Balance = customer.Balance
            };
        }
    }
}

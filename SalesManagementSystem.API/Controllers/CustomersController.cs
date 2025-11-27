using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Application.DTOs.Customers;
using SalesManagementSystem.Application.Features.Customers.Queries;

namespace SalesManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create(CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var query = new GetCustomerByIdQuery { Id = id };
            var customer = await _mediator.Send(query);

            if (customer == null)
            {
                return NotFound(); 
            }

            return Ok(customer);
        }

    }
}

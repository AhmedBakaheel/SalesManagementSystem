using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Application.Features.CustomerPayments.Commands;
using SalesManagementSystem.Application.DTOs.Customers;
using System.Threading.Tasks;

[Route("api/customer-payments")] 
[ApiController]
public class CustomerPaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerPaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerPaymentCommand command)
    {
        try
        {
            var updatedCustomer = await _mediator.Send(command);

            return Ok(updatedCustomer);
        }
        catch (Exception ex) when (ex.Message.Contains("غير موجود"))
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
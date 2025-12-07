using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Application.Features.SupplierPayments.Commands;
using SalesManagementSystem.Application.DTOs.Suppliers;
using System.Threading.Tasks;

[Route("api/supplier-payments")] 
[ApiController]
public class SupplierPaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SupplierPaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SupplierDto>> Create([FromBody] CreateSupplierPaymentCommand command)
    {
        try
        {
            var updatedSupplier = await _mediator.Send(command);

            return Ok(updatedSupplier);
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
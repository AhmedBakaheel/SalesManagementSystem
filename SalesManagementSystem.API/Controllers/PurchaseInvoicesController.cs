using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Application.Features.Purchases.Commands;
using System.Threading.Tasks;

[Route("api/[controller]")] 

[ApiController]
public class PurchaseInvoicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PurchaseInvoicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    public async Task<ActionResult<int>> Create([FromBody] CreatePurchaseInvoiceCommand command)
    {
        try
        {
            int invoiceId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPurchaseInvoiceById), new { id = invoiceId }, invoiceId);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("غير موجود"))
            {
                return NotFound(ex.Message);
            }
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public Task<ActionResult<int>> GetPurchaseInvoiceById(int id)
    {
        return Task.FromResult<ActionResult<int>>(NotFound());
    }
}
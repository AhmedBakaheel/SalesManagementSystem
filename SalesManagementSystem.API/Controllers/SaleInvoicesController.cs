using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Application.Features.Sales.Commands;
using System.Threading.Tasks;

[Route("api/sale-invoices")] 
[ApiController]
public class SaleInvoicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SaleInvoicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> Create([FromBody] CreateSaleInvoiceCommand command)
    {
        try
        {
            int invoiceId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetSaleInvoiceById), new { id = invoiceId }, invoiceId);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("غير كافٍ") || ex.Message.Contains("تجاوز حد الائتمان") || ex.Message.Contains("غير موجود"))
            {
                return BadRequest(ex.Message);
            }
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public Task<ActionResult<int>> GetSaleInvoiceById(int id)
    {
        return Task.FromResult<ActionResult<int>>(NotFound());
    }
}
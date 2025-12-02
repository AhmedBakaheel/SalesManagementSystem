using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Application.DTOs.Invoices;
using SalesManagementSystem.Application.Features.Invoices.Commands;
using SalesManagementSystem.Application.Features.Invoices.Queries;

namespace SalesManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InvoiceCreateResponse>> CreateInvoice([FromBody] CreateInvoiceCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);

                return CreatedAtAction(nameof(CreateInvoice), response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        //  /api/invoices/TotalSales
        [HttpGet("TotalSales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TotalSalesDto>> GetTotalSales()
        {
            var query = new GetTotalSalesQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
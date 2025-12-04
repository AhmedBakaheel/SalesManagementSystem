using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Application.Features.Suppliers.Commands;
using SalesManagementSystem.Application.DTOs.Suppliers;
using System.Threading.Tasks;
using SalesManagementSystem.Application.Features.Suppliers.Queries;

[Route("api/[controller]")]
[ApiController]
public class SuppliersController : ControllerBase
{
    private readonly IMediator _mediator;

    public SuppliersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SupplierDto>> Create([FromBody] CreateSupplierCommand command)
    {
        var createdSupplier = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetSupplierById), new { id = createdSupplier.Id }, createdSupplier);
    }
    //   /api/suppliers
    [HttpGet] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAll([FromQuery] GetSuppliersListQuery query)
    {
        var suppliersList = await _mediator.Send(query);

        return Ok(suppliersList);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    public async Task<ActionResult<SupplierDto>> GetSupplierById(int id)
    {
        var query = new GetSupplierByIdQuery(id);
        var supplier = await _mediator.Send(query);

        if (supplier == null)
        {
            return NotFound($"لم يتم العثور على مورد بالمعرّف: {id}");
        }

        return Ok(supplier);
    }
    //  /api/suppliers/{id}
    [HttpPut("{id}")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SupplierDto>> Update(int id, [FromBody] UpdateSupplierCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("معرّف المورد في المسار لا يتطابق مع المعرّف في الجسم.");
        }

        try
        {
            var updatedSupplier = await _mediator.Send(command);
            return Ok(updatedSupplier);
        }
        catch (Exception ex) when (ex.Message.Contains("لم يتم العثور"))
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            throw;
        }
    }
    //  /api/suppliers/{id}
    [HttpDelete("{id}")] 
    [ProducesResponseType(StatusCodes.Status204NoContent)] 
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var command = new DeleteSupplierCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
        catch (Exception ex) when (ex.Message.Contains("لم يتم العثور"))
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex) when (ex.Message.Contains("لا يمكن حذف المورد"))
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
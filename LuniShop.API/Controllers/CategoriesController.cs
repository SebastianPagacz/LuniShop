using LuniShop.Application.Categories.Commands;
using LuniShop.Application.Categories.DTO;
using LuniShop.Application.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuniShop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        if (result.IsSuccesfull)
            return Created(result.Value, result.Message);

        return BadRequest(result.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);

        if (result.IsSuccesfull)
            return Ok(result.Value);

        return NotFound(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteCategoryCommand(id), cancellationToken);

        if (result.IsSuccesfull)
            return NoContent();

        return NotFound(result.Message);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchAsync(int id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateCategoryCommand(id, request.Name, request.IsActive), cancellationToken);

        if (result.IsSuccesfull)
            return Ok(result.Value);

        return NotFound(result.Message);
    }
}

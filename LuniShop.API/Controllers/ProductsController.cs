using LuniShop.Application.Products.Commands;
using LuniShop.Application.Products.Queries;
using LuniShop.Application.Reviews.Commands;
using LuniShop.Application.Reviews.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LuniShop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] AddProductCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        if (result.IsSuccesfull)
            return Created(result.Value, result.Message); // ToDo: might return just Id instead of while object

        return StatusCode(400, result.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductByIdQuery(id), cancellationToken);

        if (result.IsSuccesfull)
            return StatusCode(200, result.Value);

        return StatusCode(404, result.Message);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllProductsQuery(), cancellationToken);

        return StatusCode(200, result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteProductCommand(id), cancellationToken);

        if (result.IsSuccesfull)
            return StatusCode(200, result.Message);

        return StatusCode(404, result.Message);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchAsync([FromBody] UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        if (result.IsSuccesfull)
            return NoContent();

        return StatusCode(404, result.Message);
    }

    #region Review
    [HttpPost("Reviews")]
    public async Task<IActionResult> PostAsync([FromBody] AddReviewCommand request, CancellationToken cancellationToken) // ToDo: change to fit API convention
    {
        var result = await mediator.Send(request, cancellationToken);

        if (result.IsSuccesfull)
            return Created(result.Value, result.Message);

        return StatusCode(400, result.Message);
    }

    [HttpGet("{productId}/Reviews")]
    public async Task<IActionResult> GetReviewsByProductIdAsync(int productId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllReviewsByProductIdQuery(productId), cancellationToken);
        
        if(result.IsSuccesfull)
            return StatusCode(200, result.Value);
        
        return StatusCode(404, result.Message);
    }

    [HttpGet("{productId}/Reviews/{reviewId}")]
    public async Task<IActionResult> GetReviewByIdAsync(int productId, int reviewId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetReviewByIdQuery(productId, reviewId), cancellationToken);

        if (result.IsSuccesfull)
            return Ok(result.Value);

        return NotFound(result.Message);
    }
    #endregion
}

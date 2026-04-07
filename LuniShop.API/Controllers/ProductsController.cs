using LuniShop.Application.Products.Commands;
using LuniShop.Application.Products.DTO;
using LuniShop.Application.Products.Queries;
using LuniShop.Application.Reviews.Commands;
using LuniShop.Application.Reviews.DTO;
using LuniShop.Application.Reviews.Queries;
using MediatR;
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

        return BadRequest(result.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductByIdQuery(id), cancellationToken);

        if (result.IsSuccesfull)
            return Ok(result.Value);

        return NotFound(result.Message);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int? categoryId, CancellationToken cancellationToken)
    {
        if (categoryId.HasValue)
        {
            var productsForCategory = await mediator.Send(new GetAllProductsByCategoryIdQuery((int)categoryId), cancellationToken);
            
            if(productsForCategory.IsSuccesfull)
                return Ok(productsForCategory.Value);

            return NotFound(productsForCategory.Message);
        }

        var products = await mediator.Send(new GetAllProductsQuery(), cancellationToken);

        return Ok(products.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteProductCommand(id), cancellationToken);

        if (result.IsSuccesfull)
            return Ok(result.Message);

        return NotFound(result.Message);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchAsync([FromBody] UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        if (result.IsSuccesfull)
            return NoContent();

        return NotFound(result.Message);
    }

    #region Review
    [HttpPost("reviews")]
    public async Task<IActionResult> PostAsync([FromBody] AddReviewCommand request, CancellationToken cancellationToken) // ToDo: change to fit API convention
    {
        var result = await mediator.Send(request, cancellationToken);

        if (result.IsSuccesfull)
            return Created(result.Value, result.Message);

        return BadRequest(result.Message);
    }

    [HttpGet("{productId}/reviews")]
    public async Task<IActionResult> GetReviewsByProductIdAsync(int productId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllReviewsByProductIdQuery(productId), cancellationToken);

        if (result.IsSuccesfull)
            return Ok(result.Value);

        return NotFound(result.Message);
    }

    [HttpGet("{productId}/reviews/{reviewId}")]
    public async Task<IActionResult> GetReviewByIdAsync(int productId, int reviewId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetReviewByIdQuery(productId, reviewId), cancellationToken);

        if (result.IsSuccesfull)
            return Ok(result.Value);

        return NotFound(result.Message);
    }

    [HttpPatch("{productId}/reviews/{reviewId}")]
    public async Task<IActionResult> PatchAsync(int productId, int reviewId, [FromBody] UpdateReviewRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateReviewCommand(reviewId, request.Title, request.Content, request.Rating, productId), cancellationToken);

        if (result.IsSuccesfull)
            return Ok(result.Message);

        return NotFound(result.Message);
    }

    [HttpDelete("{productId}/reviews/{reviewId}")]
    public async Task<IActionResult> DeleteAsync(int productId, int reviewId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteReviewCommand(productId, reviewId), cancellationToken);

        if (result.IsSuccesfull)
            return NoContent();

        return NotFound(result.Message);
    }
    #endregion
    #region Category
    [HttpPost("{productId}/categories")]
    public async Task<IActionResult> AssignCategoryAsync(int productId, [FromBody] AssignCategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new AssignCategoryCommand(productId, request.CategoryId), cancellationToken);

        if (result.IsSuccesfull)
            return Created(result.Value, result.Message);

        return NotFound(result.Message);
    }
    #endregion
}

using LuniShop.Application.Products.Commands;
using LuniShop.Application.Products.Queries;
using LuniShop.Application.Reviews.Commands;
using LuniShop.Application.Reviews.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LuniShop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] AddProductCommand request)
    {
        var result = await mediator.Send(request);

        if (result.IsSuccesfull)
            return StatusCode(201, $"Product with Id: {result.Value.Id} was successfuly created."); // ToDo: might return just Id instead of while object

        return StatusCode(400, result.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await mediator.Send(new GetProductByIdQuery(id));

        if (result.IsSuccesfull)
            return StatusCode(200, result.Value);

        return StatusCode(404, result.Message);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await mediator.Send(new GetAllProductsQuery());

        return StatusCode(200, result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await mediator.Send(new DeleteProductCommand(id));

        if (result.IsSuccesfull)
            return StatusCode(200, result.Message);

        return StatusCode(404, result.Message);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchAsync([FromBody] UpdateProductCommand request)
    {
        var result = await mediator.Send(request);

        if (result.IsSuccesfull)
            return StatusCode(200, result.Value);

        return StatusCode(404, result.Message);
    }

    #region Review
    [HttpPost("Reviews")]
    public async Task<IActionResult> PostAsync([FromBody] AddReviewCommand request) // ToDo: change to fit API convention
    {
        var result = await mediator.Send(request);

        if (result.IsSuccesfull)
            return StatusCode(201, $"Review with Id: {result.Value.Id} was successfuly created."); // ToDo: might return just Id instead of while object

        return StatusCode(400, result.Message);
    }

    [HttpGet("{productId}/Reviews")]
    public async Task<IActionResult> GetReviewsByProductIdAsync(int productId)
    {
        var result = await mediator.Send(new GetAllReviewsByProductIdQuery(productId));
        
        if(result.IsSuccesfull)
            return StatusCode(200, result.Value);
        
        return StatusCode(404, result.Message);
    }
    #endregion
}

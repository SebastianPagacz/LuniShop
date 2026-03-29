using LuniShop.Application.Products.Commands;
using LuniShop.Application.Products.Queries;
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
            return StatusCode(201, result.Value);

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
            return StatusCode(201, result.Message);

        return StatusCode(404, result.Message);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchAsync([FromBody] UpdateProductCommand request)
    {
        var result = await mediator.Send(request);

        if(result.IsSuccesfull)
            return StatusCode(200, result.Value);

        return StatusCode(404, result.Message);
    }
}

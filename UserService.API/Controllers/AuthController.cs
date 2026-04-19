using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Users.Command;

namespace UserService.API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(request, cancellationToken);

            if(result.IsSuccesfull)
                return Created();

            return BadRequest(result.Message);
        }
    }
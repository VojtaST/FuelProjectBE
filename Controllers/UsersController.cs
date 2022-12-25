using FuelProject.Domain.DTos;
using FuelProject.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected IMediator _mediator { get; }

        [HttpPost("login")]
        public async Task<ActionResult<LoginUserDto>> LoginUser([FromBody] LoginUserCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("register")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<LoginUserDto>> RegisterUser([FromBody] RegisterUserCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}

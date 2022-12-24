using FuelProject.Cars.Commands;
using FuelProject.Cars.Commands.DeleteCar;
using FuelProject.Cars.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class CarsController : ControllerBase
    {
        public CarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected IMediator _mediator { get; }

        [HttpGet("cars-user")]
        public async Task<ActionResult> GetCarsForUser([FromQuery] string userId)
        {
            GetCarsForUserQuery query = new(userId);
            return Ok(await _mediator.Send(query));
        }


        [HttpPost("add-car")]
        public async Task<ActionResult> AddCar([FromBody] AddCarCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}/edit")]
        public async Task<ActionResult> EditCar([FromRoute] string id, [FromBody] EditCarCommand command)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> DeleteCar([FromRoute] string id)
        {
            return Ok(await _mediator.Send(new DeteleCarCommand(id)));
        }
    }
}

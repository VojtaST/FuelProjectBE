using FuelProject.Cars.Commands;
using FuelProject.Cars.Commands.AddUser;
using FuelProject.Cars.Commands.DeleteCar;
using FuelProject.Cars.Queries;
using FuelProject.Domain.DTos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Controllers;

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
    public async Task<ActionResult<List<CarDto>>> GetCarsForUser([FromQuery] string userId)
    {
        GetCarsForUserQuery query = new(userId);
        return await _mediator.Send(query);
    }


    [HttpPost("add-car")]
    public async Task<ActionResult> AddCar([FromBody] AddCarCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut("{id}/edit")]
    public async Task<ActionResult> EditCar([FromRoute] string id, [FromBody] EditCarCommand command)
    {
        command.Id = id;
        return await _mediator.Send(command);
    }

    [HttpDelete("{id}/delete")]
    public async Task<ActionResult> DeleteCar([FromRoute] string id)
    {
        return await _mediator.Send(new DeteleCarCommand(id));
    }

    [HttpPost("{id}/add-user")]
    public async Task<ActionResult> AddUserToCar([FromRoute] string id, [FromBody] AddUserToCarCommand command)
    {
        command.CarId=id;
        return await _mediator.Send(command);
    }
}

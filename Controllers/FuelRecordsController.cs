using FuelProject.Domain.DTos;
using FuelProject.FuelRecords.Commands.AddFuelRecord;
using FuelProject.FuelRecords.Commands.DeleteFuelRecord;
using FuelProject.FuelRecords.Commands.EditFuelRecord;
using FuelProject.FuelRecords.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class FuelRecordsController : ControllerBase
{
    public FuelRecordsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected IMediator _mediator { get; }

    [HttpPost("add-fuel-record")]
    public async Task<ActionResult> AddFuelRecord([FromBody] AddFuelRecordCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet("fuel-records-car")]
    public async Task<ActionResult<List<FuelRecordDto>>> GetFuelRecordsPerCar([FromQuery] string CarId)
    {
        GetFuelRecordsForCarQuery query = new(CarId);
        return await _mediator.Send(query);
    }

    [HttpGet("fuel-records-user")]
    public async Task<ActionResult<List<FuelRecordDto>>> GetFuelRecordsPerUser([FromQuery] string UserId)
    {
        GetFuelRecordsForUserQuery query = new(UserId);
        return await _mediator.Send(query);
    }

    [HttpPut("{id}/edit")]
    public async Task<ActionResult> EditFuelRecord([FromRoute] string id, [FromBody] EditFuelRecordCommand command)
    {
        command.Id = id;
        return await _mediator.Send(command);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FuelRecordDto>> GetFuelRecord([FromRoute] string id)
    {
        GetFuelRecordQuery query = new(id);
        return await _mediator.Send(query);
    }

    [HttpDelete("{id}/delete")]
    public async Task<ActionResult<FuelRecordDto>> DeleteFuelRecord([FromRoute] string id)
    {
        DeleteFuelRecordCommand command = new(id);
        return await _mediator.Send(command);
    }
}

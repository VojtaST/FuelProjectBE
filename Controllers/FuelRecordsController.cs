using FuelProject.Domain.DTos;
using FuelProject.FuelRecords.Commands;
using FuelProject.FuelRecords.Queries;
using FuelProject.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController,Authorize]
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
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("fuel-records-car")]
        public async Task<ActionResult> GetFuelRecordsPerCar([FromQuery] string CarId)
        {
            GetFuelRecordsForCarQuery query = new(CarId);
            return Ok(await _mediator.Send(query));
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
            return Ok(await _mediator.Send(command));
        }
    }
}

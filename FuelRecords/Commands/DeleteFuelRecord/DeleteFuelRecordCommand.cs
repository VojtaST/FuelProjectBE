using FuelProject.Repositories;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.FuelRecords.Commands.DeleteFuelRecord;

[MediatRBehavior(typeof(ValidateFuelRecordExistsBehavior<DeleteFuelRecordCommand>))]
public class DeleteFuelRecordCommand : IRequest<ActionResult>, IFuelRecordById
{
    public DeleteFuelRecordCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}


public class DeleteFuelRecordCommandHandler : IRequestHandler<DeleteFuelRecordCommand, ActionResult>
{
    private readonly IFuelRecordRepository _repository;

    public DeleteFuelRecordCommandHandler(IFuelRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<ActionResult> Handle(DeleteFuelRecordCommand request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.Id);
        return new OkResult();
    }
}

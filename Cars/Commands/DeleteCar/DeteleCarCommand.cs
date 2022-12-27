using FuelProject.Domain.DTos;
using FuelProject.Repositories;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Cars.Commands.DeleteCar;

[MediatRBehavior(typeof(ValidateCarExistsBehavior<DeteleCarCommand>))]
public class DeteleCarCommand : IRequest<ActionResult>, ICarById
{
    public DeteleCarCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}

public class DeleteCarCommandHandler : IRequestHandler<DeteleCarCommand, ActionResult>
{
    private readonly ICarRepository _carRepository;

    public DeleteCarCommandHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<ActionResult> Handle(DeteleCarCommand request, CancellationToken cancellationToken)
    {
        await _carRepository.Delete(request.Id);
        return new OkResult();
    }
}

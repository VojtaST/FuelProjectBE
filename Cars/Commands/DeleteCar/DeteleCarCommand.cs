using FuelProject.Cars.Commands.EditCar;
using FuelProject.Repositories;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Cars.Commands.DeleteCar;

[MediatRBehavior(typeof(ValidateCarExistsBehavior))]
public record DeteleCarCommand(string Id) : IRequest<ActionResult>;
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

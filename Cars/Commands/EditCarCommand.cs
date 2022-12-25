using FuelProject.Cars.Commands.EditCar;
using FuelProject.Domain.Entities;
using FuelProject.Repositories;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Cars.Commands;

[MediatRBehavior(typeof(ValidateCarExistsBehavior))]
public class EditCarCommand : IRequest<ActionResult>
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; }
    public string LicencePlate { get; set; }
    public FuelType FuelType { get; set; }
}

public class EditCarCommandHandler : IRequestHandler<EditCarCommand, ActionResult>
{
    private readonly ICarRepository _carRepository;

    public EditCarCommandHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<ActionResult> Handle(EditCarCommand request, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetCarById(request.Id);

        car.FuelType = request.FuelType;
        car.Name = request.Name;
        car.LicencePlate = request.LicencePlate;
        await _carRepository.Update(car);

        return new OkResult();
    }
}

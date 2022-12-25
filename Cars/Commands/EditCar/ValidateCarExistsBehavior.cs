using FuelProject.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Cars.Commands.EditCar;

public class ValidateCarExistsBehavior : IPipelineBehavior<EditCarCommand, ActionResult>
{
    private readonly ICarRepository _carRepository;

    public ValidateCarExistsBehavior(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<ActionResult> Handle(EditCarCommand request, RequestHandlerDelegate<ActionResult> next, CancellationToken cancellationToken)
    {
       var car = await _carRepository.GetCarById(request.Id);
        if (car is null)
        {
            return new NotFoundObjectResult("Car not found");
        }
        return await next();
    }
}

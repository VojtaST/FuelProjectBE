using FuelProject.Domain.DTos;
using FuelProject.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Cars.Commands;

public class ValidateCarExistsBehavior<TRequest> : IPipelineBehavior<TRequest, ActionResult>
    where TRequest : IRequest<ActionResult>, ICarById
{
    private readonly ICarRepository _carRepository;

    public ValidateCarExistsBehavior(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<ActionResult> Handle(TRequest request, RequestHandlerDelegate<ActionResult> next, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetCarById(request.Id);
        if (car is null)
        {
            return new NotFoundObjectResult("Car not found");
        }
        return await next();
    }
}

using FuelProject.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Cars.Commands.EditCar
{
    public class ValidateCarExistsBehavior : IPipelineBehavior<EditCarCommand, Unit>
    {
        private readonly ICarRepository _carRepository;

        public ValidateCarExistsBehavior(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Unit> Handle(EditCarCommand request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
        {
           var car = await _carRepository.GetCarById(request.Id);
            if (car is null)
            {
               // throw new Exception("Not found");
                throw new HttpRequestException("Car not found");
            }
            return await next();
        }
    }
}

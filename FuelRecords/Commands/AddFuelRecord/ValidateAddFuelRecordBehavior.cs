using FuelProject.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.FuelRecords.Commands.AddFuelRecord
{
    public class ValidateAddFuelRecordBehavior : IPipelineBehavior<AddFuelRecordCommand, ActionResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;

        public ValidateAddFuelRecordBehavior(IUserRepository userRepository, ICarRepository carRepository)
        {
            _userRepository = userRepository;
            _carRepository = carRepository;
        }

        public async Task<ActionResult> Handle(AddFuelRecordCommand request, RequestHandlerDelegate<ActionResult> next, CancellationToken cancellationToken)
        {
            if ((await _userRepository.GetUserById(request.UserId)) is null)
            {
                return new NotFoundObjectResult($"User with id: {request.UserId} was not found");
            }

            if ((await _carRepository.GetCarById(request.CarId)) is null)
            {
                return new NotFoundObjectResult($"Car with id: {request.CarId} was not found");
            }
            return await next();
        }
    }
}
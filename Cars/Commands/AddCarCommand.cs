using FuelProject.Domain.Entities;
using FuelProject.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Cars.Commands
{
    public class AddCarCommand : IRequest<ActionResult>
    {
        public string Name { get; set; }
        public string LicencePlate { get; set; }
        public FuelType FuelType { get; set; }
        public string UserId { get; set; }
    }

    public class AddCarCommandHandler : IRequestHandler<AddCarCommand, ActionResult>
    {
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;

        public AddCarCommandHandler(ICarRepository carRepository, IUserRepository userRepository)
        {
            _carRepository = carRepository;
            _userRepository = userRepository;
        }

        public async Task<ActionResult> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId);
            Car car = new()
            {
                LicencePlate = request.LicencePlate,
                FuelType = request.FuelType,
                Name = request.Name,
                FuelRecords = new List<FuelRecord>(),
                Users = new List<User>() { user },
            };
            await _carRepository.Add(car);
            return new OkResult();
        }
    }
}

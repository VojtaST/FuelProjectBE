using FuelProject.Cars.Commands.EditCar;
using FuelProject.Repositories;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace FuelProject.Cars.Commands.DeleteCar
{
    [MediatRBehavior(typeof(ValidateCarExistsBehavior))]
    public record DeteleCarCommand(string Id) : IRequest;
    public class DeleteCarCommandHandler : IRequestHandler<DeteleCarCommand>
    {
        private readonly ICarRepository _carRepository;

        public DeleteCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Unit> Handle(DeteleCarCommand request, CancellationToken cancellationToken)
        {
            await _carRepository.Delete(request.Id);
            return Unit.Value;
        }
    }
}

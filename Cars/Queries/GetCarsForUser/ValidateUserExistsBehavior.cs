using FuelProject.Domain.DTos;
using FuelProject.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Cars.Queries.GetCarsForUser
{
    public class ValidateUserExistsBehavior : IPipelineBehavior<GetCarsForUserQuery, ActionResult<List<CarDto>>>
    {
        private readonly IUserRepository _userRepository;

        public ValidateUserExistsBehavior(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ActionResult<List<CarDto>>> Handle(GetCarsForUserQuery request, RequestHandlerDelegate<ActionResult<List<CarDto>>> next, CancellationToken cancellationToken)
        {
            if ((await _userRepository.GetUserById(request.UserId)) is null)
            {
                return new NotFoundObjectResult($"User with id: {request.UserId} was not found");
            }
            return await next();
        }
    }
}

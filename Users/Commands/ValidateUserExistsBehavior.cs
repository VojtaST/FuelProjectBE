using FuelProject.Domain.DTos;
using FuelProject.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Users.Commands
{
    public class ValidateUserExistsBehavior : IPipelineBehavior<LoginUserCommand, ActionResult<LoginUserDto>>
    {
        private readonly IUserRepository _userRepository;

        public ValidateUserExistsBehavior(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ActionResult<LoginUserDto>> Handle(LoginUserCommand request, RequestHandlerDelegate<ActionResult<LoginUserDto>> next, CancellationToken cancellationToken)
        {
            if ((await _userRepository.GetUserByUsername(request.userName)) is null)
            {
                return new NotFoundObjectResult($"User with username: {request.userName} was not found");
            }
            return await next();
        }
    }
}

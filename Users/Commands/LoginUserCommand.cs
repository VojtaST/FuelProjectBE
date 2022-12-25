using FuelProject.Domain.DTos;
using FuelProject.Repositories;
using FuelProject.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Users.Commands;

public record LoginUserCommand(string userName, string password) : IRequest<ActionResult<LoginUserDto>>;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ActionResult<LoginUserDto>>
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(IUserService userService, IUserRepository userRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    public async Task<ActionResult<LoginUserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userService.CheckCredentials(request.userName, request.password))
        {
            //vygenerovat token
            var user = await _userRepository.GetUserByUsername(request.userName);
            var authResult = await _userService.GenerateToken();

            return new OkObjectResult(new LoginUserDto(user.Id.ToString(), authResult));
        }
        return new BadRequestResult();
    }
}
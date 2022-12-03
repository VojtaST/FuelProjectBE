using FuelProject.Domain.DTos;
using FuelProject.Infrastructure;
using FuelProject.Repositories;
using FuelProject.Services;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FuelProject.Users.Commands;

public record LoginUserCommand(string userName, string password) : IRequest<LoginUserDto>;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserDto>
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(IUserService userService,IUserRepository userRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    public async Task<LoginUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userService.CheckCredentials(request.userName, request.password))
        {
            //vygenerovat token
            var user = await _userRepository.GetUserByUsername(request.userName);          
            var authResult = await _userService.GenerateToken();

            return new LoginUserDto(user.Id.ToString(), authResult);
        }
        return null;
    }
}
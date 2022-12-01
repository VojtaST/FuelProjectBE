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

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManagerCustom.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5235",
                audience: "http://localhost:5235",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);


            return new LoginUserDto(user.Id.ToString(), tokenString);
        }
        return null;
    }
}
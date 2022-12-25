using FuelProject.Domain.Entities;
using FuelProject.Infrastructure;
using FuelProject.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCR = BCrypt.Net.BCrypt;

namespace FuelProject.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> CheckCredentials(string username, string password)
    {
        var user = await _userRepository.GetUserByUsername(username);
        if (user is not null && BCR.Verify(password,user.Password)) return true;
        return false;
    }

    public Task<User> CreateUser(string firstName, string surname, string password, string username)
    {
        return Task.FromResult(new User()
        {
            FirstName = firstName,
            Surname = surname,
            Password = BCR.HashPassword(password, 4),
            UserName = username,
            Cars = new List<Car>()
        });
    }

    public Task<string> GenerateToken()
    {
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
        return Task.FromResult(tokenString);
    }
}

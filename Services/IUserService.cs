using FuelProject.Domain.Entities;

namespace FuelProject.Services;

public interface IUserService
{
    Task<bool> CheckCredentials(string username, string password);
    Task<string> GenerateToken();
    Task<User> CreateUser(string firstName, string surname, string password, string username);
}

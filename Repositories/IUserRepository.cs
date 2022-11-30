using FuelProject.Domain.Entities;

namespace FuelProject.Repositories;

public interface IUserRepository
{
    Task<User> GetUserById(string Id);
    Task<User> GetUserByUsername(string username);
}

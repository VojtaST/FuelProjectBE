using FuelProject.Domain.Entities;

namespace FuelProject.Repositories
{
    public interface ICarRepository
    {
        Task<Car> GetCarById(string id);
        Task<IEnumerable<Car>> GetCarsForUser(string userId);
        Task Add(Car car);
        Task Update(Car car);
        Task Delete(string id);
    }
}

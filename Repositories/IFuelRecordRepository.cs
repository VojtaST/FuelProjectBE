using FuelProject.Domain.Entities;

namespace FuelProject.Repositories
{
    public interface IFuelRecordRepository
    {
        Task Add(FuelRecord fuelRecord);
        Task <IEnumerable<FuelRecord>> GetFuelRecordsForCar(string carId);
        Task <IEnumerable<FuelRecord>> GetFuelRecordsForUser(string UserId);
    }
}

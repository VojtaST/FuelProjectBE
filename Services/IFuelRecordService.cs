using FuelProject.Domain.Entities;
using FuelProject.FuelRecords.Commands;

namespace FuelProject.Services
{
    public interface IFuelRecordService
    {
        Task<FuelRecord> CreateFuelRecord(AddFuelRecordCommand request, string userId);
    }
}

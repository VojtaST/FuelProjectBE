using FuelProject.Domain.Entities;
using FuelProject.FuelRecords.Commands.AddFuelRecord;

namespace FuelProject.Services;

public interface IFuelRecordService
{
    Task<FuelRecord> CreateFuelRecord(AddFuelRecordCommand request, string userId);
}

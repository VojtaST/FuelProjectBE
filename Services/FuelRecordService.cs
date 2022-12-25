using FuelProject.Domain.Entities;
using FuelProject.FuelRecords.Commands;
using FuelProject.Repositories;

namespace FuelProject.Services;

public class FuelRecordService : IFuelRecordService
{
    private readonly ICarRepository _carRepository;
    private readonly IUserRepository _userRepository;

    public FuelRecordService(ICarRepository carRepository, IUserRepository userRepository)
    {
        _carRepository = carRepository;
        _userRepository = userRepository;
    }

    public async Task<FuelRecord> CreateFuelRecord(AddFuelRecordCommand request, string userId)
    {
        var car = await _carRepository.GetCarById(request.CarId);
        var user = await _userRepository.GetUserById(userId);

        var fuelRecord = new FuelRecord()
        {
            Car = car,
            User = user,
            DashboardKms = request.DashboardKms,
            FuelAmount = request.FuelAmount,
            NameOfFuelStation = request.NameOfFuelStation,
            PricePerLiter = request.PricePerLiter,
            TotalPrice = request.TotalPrice,
            DateOfRefuel = request.DateOfRefuel
        };
        return fuelRecord;
    }
}

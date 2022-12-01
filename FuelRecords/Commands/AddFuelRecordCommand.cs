using FuelProject.Repositories;
using FuelProject.Services;
using MediatR;

namespace FuelProject.FuelRecords.Commands;

public class AddFuelRecordCommand : IRequest
{
    public string NameOfFuelStation { get; set; }
    public float FuelAmount { get; set; }
    public int DashboardKms { get; set; }
    public float PricePerLiter { get; set; }
    public float TotalPrice { get; set; }
    public string CarId { get; set; }
    public DateTime DateOfRefuel { get; set; }
    public string UserId { get; set; }
}

public class AddFuelRecordCommandHandler : IRequestHandler<AddFuelRecordCommand>
{
    private readonly IFuelRecordRepository _fuelRecordRepository;
    private readonly IFuelRecordService _fuelRecordService;

    public AddFuelRecordCommandHandler(IFuelRecordRepository fuelRecordRepository, IFuelRecordService fuelRecordService)
    {
        _fuelRecordRepository = fuelRecordRepository;
        _fuelRecordService = fuelRecordService;
    }

    public async Task<Unit> Handle(AddFuelRecordCommand request, CancellationToken cancellationToken)
    {
        var fuelRecord = await _fuelRecordService.CreateFuelRecord(request, request.UserId);

        await _fuelRecordRepository.Add(fuelRecord);
        return Unit.Value;
    }
}

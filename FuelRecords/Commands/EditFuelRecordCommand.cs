using FuelProject.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.FuelRecords.Commands
{
    public class EditFuelRecordCommand : IRequest<ActionResult>
    {
        public string Id { get; set; } = string.Empty;
        public string NameOfFuelStation { get; set; }
        public float FuelAmount { get; set; }
        public int DashboardKms { get; set; }
        public float PricePerLiter { get; set; }
        public float TotalPrice { get; set; }
        public string CarId { get; set; }
        public DateTime DateOfRefuel { get; set; }
    }
    public class EditFuelRecordCommandHandler : IRequestHandler<EditFuelRecordCommand, ActionResult>
    {
        private readonly IFuelRecordRepository _fuelRecordRepository;
        private readonly ICarRepository _carRepository;


        public EditFuelRecordCommandHandler(IFuelRecordRepository fuelRecordRepository)
        {
            _fuelRecordRepository = fuelRecordRepository;
        }

        public async Task<ActionResult> Handle(EditFuelRecordCommand request, CancellationToken cancellationToken)
        {
            var fuelRecord = await _fuelRecordRepository.Get(request.Id);
            if (fuelRecord.Car.Id.ToString() != request.CarId)
            {
                fuelRecord.Car = await _carRepository.GetCarById(request.CarId);
            }

            fuelRecord.DateOfRefuel = request.DateOfRefuel;
            fuelRecord.NameOfFuelStation = request.NameOfFuelStation;
            fuelRecord.FuelAmount = request.FuelAmount;
            fuelRecord.TotalPrice = request.TotalPrice;
            fuelRecord.DashboardKms = request.DashboardKms;
            fuelRecord.PricePerLiter = request.PricePerLiter;

            await _fuelRecordRepository.Update(fuelRecord);

            return new OkResult();
        }
    }
}

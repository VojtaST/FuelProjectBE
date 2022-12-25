using AutoMapper;
using FuelProject.Domain.DTos;
using FuelProject.Domain.Entities;
using FuelProject.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.FuelRecords.Queries;

public record GetFuelRecordsForCarQuery(string CarId) : IRequest<ActionResult<List<FuelRecordDto>>>;
public class GetFuelRecordsForCarQueryHandler : IRequestHandler<GetFuelRecordsForCarQuery, ActionResult<List<FuelRecordDto>>>
{
    private readonly IFuelRecordRepository _fuelRecordRepository;
    private readonly IMapper _mapper;

    public GetFuelRecordsForCarQueryHandler(IFuelRecordRepository fuelRecordRepository, IMapper mapper)
    {
        _fuelRecordRepository = fuelRecordRepository;
        _mapper = mapper;
    }

    public async Task<ActionResult<List<FuelRecordDto>>> Handle(GetFuelRecordsForCarQuery request, CancellationToken cancellationToken)
    {
        var redords = (await _fuelRecordRepository.GetFuelRecordsForCar(request.CarId)).OrderBy(x => x.DateOfRefuel).ToList();

        return new OkObjectResult(_mapper.Map<List<FuelRecord>, List<FuelRecordDto>>(redords));
    }
}

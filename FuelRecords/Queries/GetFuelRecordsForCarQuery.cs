using AutoMapper;
using FuelProject.Domain.DTos;
using FuelProject.Domain.Entities;
using FuelProject.Repositories;
using MediatR;

namespace FuelProject.FuelRecords.Queries;

public record GetFuelRecordsForCarQuery(string CarId) : IRequest<List<FuelRecordDto>>;
public class GetFuelRecordsForCarQueryHandler : IRequestHandler<GetFuelRecordsForCarQuery, List<FuelRecordDto>>
{
    private readonly IFuelRecordRepository _fuelRecordRepository;
    private readonly IMapper _mapper;

    public GetFuelRecordsForCarQueryHandler(IFuelRecordRepository fuelRecordRepository, IMapper mapper)
    {
        _fuelRecordRepository = fuelRecordRepository;
        _mapper = mapper;
    }

    public async Task<List<FuelRecordDto>> Handle(GetFuelRecordsForCarQuery request, CancellationToken cancellationToken)
    {
        var redords = (await _fuelRecordRepository.GetFuelRecordsForCar("df348454-efd6-454e-ae3c-7323d4d6037a")).ToList();

        return _mapper.Map<List<FuelRecord>, List<FuelRecordDto>>(redords);       
    }
}

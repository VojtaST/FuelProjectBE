using AutoMapper;
using FuelProject.Domain.DTos;
using FuelProject.Domain.Entities;
using FuelProject.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.FuelRecords.Queries;

public record GetFuelRecordQuery(string Id) : IRequest<ActionResult<FuelRecordDto>>;

public class GetFuelRecordQueryHandler : IRequestHandler<GetFuelRecordQuery, ActionResult<FuelRecordDto>>
{
    private readonly IFuelRecordRepository _fuelRecordRepository;
    private readonly IMapper _mapper;

    public GetFuelRecordQueryHandler(IFuelRecordRepository fuelRecordRepository, IMapper mapper)
    {
        _fuelRecordRepository = fuelRecordRepository;
        _mapper = mapper;
    }

    public async Task<ActionResult<FuelRecordDto>> Handle(GetFuelRecordQuery request, CancellationToken cancellationToken)
    {
        var record = await _fuelRecordRepository.Get(request.Id);
        return new OkObjectResult(_mapper.Map<FuelRecord, FuelRecordDto>(record));
    }
}

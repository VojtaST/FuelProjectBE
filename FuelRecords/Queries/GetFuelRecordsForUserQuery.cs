using AutoMapper;
using FuelProject.Domain.DTos;
using FuelProject.Domain.Entities;
using FuelProject.Repositories;
using MediatR;

namespace FuelProject.FuelRecords.Queries
{
    public record GetFuelRecordsForUserQuery(string UserId):IRequest<List<FuelRecordDto>>;

    public class GetFuelRecordsForUserQueryHandler : IRequestHandler<GetFuelRecordsForUserQuery, List<FuelRecordDto>>
    {
        private readonly IFuelRecordRepository _fuelRecordRepository;
        private readonly IMapper _mapper;

        public GetFuelRecordsForUserQueryHandler(IFuelRecordRepository fuelRecordRepository, IMapper mapper)
        {
            _fuelRecordRepository = fuelRecordRepository;
            _mapper = mapper;
        }

        public async Task<List<FuelRecordDto>> Handle(GetFuelRecordsForUserQuery request, CancellationToken cancellationToken)
        {
            var redords = (await _fuelRecordRepository.GetFuelRecordsForUser(request.UserId)).ToList();

            return _mapper.Map<List<FuelRecord>, List<FuelRecordDto>>(redords);
        }
    }
}

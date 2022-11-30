using AutoMapper;
using FuelProject.Domain.DTos;
using FuelProject.Domain.Entities;

namespace FuelProject.Domain.Profiles
{
    public class FuelRecordsProfile : Profile
    {
        public FuelRecordsProfile()
        {
            CreateMap<FuelRecord, FuelRecordDto>();
        }
    }
}

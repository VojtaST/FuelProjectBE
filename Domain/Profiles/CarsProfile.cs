using AutoMapper;
using FuelProject.Domain.DTos;
using FuelProject.Domain.Entities;

namespace FuelProject.Domain.Profiles
{
    public class CarsProfile : Profile
    {
        public CarsProfile()
        {
            CreateMap<Car, CarDto>();
        }
    }
}

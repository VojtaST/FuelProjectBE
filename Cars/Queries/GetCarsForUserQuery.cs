using AutoMapper;
using FuelProject.Domain.DTos;
using FuelProject.Domain.Entities;
using FuelProject.Infrastructure;
using FuelProject.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace FuelProject.Cars.Queries
{
    public record GetCarsForUserQuery(string UserId) : IRequest<List<CarDto>>;

    public class GetCarsForUserQueryHandler : IRequestHandler<GetCarsForUserQuery, List<CarDto>>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly DiaryDbContext _context;

        public GetCarsForUserQueryHandler(ICarRepository carRepository, IMapper mapper, DiaryDbContext context)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<CarDto>> Handle(GetCarsForUserQuery request, CancellationToken cancellationToken)
        {
            //var lsit = new List<CarDto>() { new CarDto() { Id = "idAuta", Name = "Astra",LicencePlate="7T22233" } };

            // return Task.FromResult(lsit);

            var query = (await _context.Users.Include(x=>x.Cars).Where(x => x.Id.ToString() == request.UserId).FirstAsync()).Cars;
            //var query1 = await _carRepository.GetCarsForUser("3b23416f-142d-4631-9668-21db6a646e94");
            return _mapper.Map<List<Car>, List<CarDto>>(query);
           // return await _mapper.ProjectTo<List<CarDto>>(query).ToListAsync();

            //return await _mapper.Map(IEnumerable<Car>,List<CarDto>)((await _carRepository.GetCarsForUser("3b23416f-142d-4631-9668-21db6a646e94"));
            //return (await _carRepository.GetCarsForUser("3b23416f-142d-4631-9668-21db6a646e94")).ToList();
        }
    }
}

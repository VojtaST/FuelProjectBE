using FuelProject.Domain.Entities;
using FuelProject.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FuelProject.Repositories;

public class CarRepository : ICarRepository
{
    private readonly DiaryDbContext _context;

    public CarRepository(DiaryDbContext context)
    {
        _context = context;
    }

    public async Task Add(Car car)
    {
        _context.Add(car);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(string id)
    {
        var car = await GetCarById(id);
        _context.Cars.Remove(car);
        _context.SaveChanges();
    }

    public Task<Car?> GetCarById(string id)
    {
        return _context.Cars.Include(x => x.Users).FirstOrDefaultAsync(c => c.Id.ToString() == id);
    }

    public async Task<IEnumerable<Car>> GetCarsForUser(string userId)
    {

        return (await _context.Users.Where(x => x.Id.ToString() == userId).FirstAsync()).Cars;
    }

    public async Task Update(Car car)
    {
        _context.Update(car);
        await _context.SaveChangesAsync();
    }
}

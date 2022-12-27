using FuelProject.Domain.Entities;
using FuelProject.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FuelProject.Repositories;

public class FuelRecordRepository : IFuelRecordRepository
{
    private readonly DiaryDbContext _context;

    public FuelRecordRepository(DiaryDbContext context)
    {
        _context = context;
    }

    public async Task Add(FuelRecord fuelRecord)
    {
        _context.Add(fuelRecord);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(string id)
    {
        var record = await Get(id);
        _context.FuelRecords.Remove(record);
        await _context.SaveChangesAsync();
    }

    public async Task<FuelRecord?> Get(string id)
    {
        return await _context.FuelRecords.Include(x => x.Car).FirstOrDefaultAsync(x => x.Id.ToString() == id);
    }

    public async Task<IEnumerable<FuelRecord>> GetFuelRecordsForCar(string carId)
    {
        return await _context.FuelRecords.Include(x => x.Car).Where(x => x.Car.Id.ToString() == carId).ToListAsync();
    }

    public async Task<IEnumerable<FuelRecord>> GetFuelRecordsForUser(string UserId)
    {
        return (await _context.Users.Include(x => x.Cars).ThenInclude(x => x.FuelRecords).FirstAsync(x => x.Id.ToString() == UserId)).Cars.SelectMany(x => x.FuelRecords);
    }

    public async Task Update(FuelRecord fuelRecord)
    {
        _context.Update(fuelRecord);
        await _context.SaveChangesAsync();
    }
}

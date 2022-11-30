using FuelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuelProject.Infrastructure;

public class DiaryDbContext : DbContext
{
    public DiaryDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Car> Cars => Set<Car>();
    public DbSet<User> Users => Set<User>();
    public DbSet<FuelRecord> FuelRecords => Set<FuelRecord>();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        //this.SetAuditableProperties(userId, _dateTimeService);
        //HandleDomainEvents();
        return base.SaveChangesAsync(cancellationToken);
    }
}

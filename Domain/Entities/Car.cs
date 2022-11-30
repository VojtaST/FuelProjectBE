namespace FuelProject.Domain.Entities
{
    public class Car
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string LicencePlate { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty ;
        public FuelType FuelType { get; set; }
        public List<FuelRecord> FuelRecords { get; set; }
        public List<User> Users { get; set; }
    }
}

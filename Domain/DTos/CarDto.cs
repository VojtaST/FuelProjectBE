using FuelProject.Domain.Entities;

namespace FuelProject.Domain.DTos
{
    public class CarDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public FuelType FuelType { get; set; }
        public string LicencePlate { get; set; }
    }
}

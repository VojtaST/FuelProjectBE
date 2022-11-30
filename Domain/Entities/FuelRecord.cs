namespace FuelProject.Domain.Entities;

public class FuelRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string NameOfFuelStation { get; set; } = string.Empty;
    public float FuelAmount { get; set; }
    public int DashboardKms { get; set; }
    public float PricePerLiter { get; set; }
    public float TotalPrice { get; set; }
    public Car Car { get; set; }
    public User User { get; set; }
}

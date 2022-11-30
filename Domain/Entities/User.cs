namespace FuelProject.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Car> Cars { get; set; }
    }
}

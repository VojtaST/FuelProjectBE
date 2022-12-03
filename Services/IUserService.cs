namespace FuelProject.Services
{
    public interface IUserService
    {
        Task<bool> CheckCredentials(string username, string password);
        Task<string> GenerateToken();
    }
}

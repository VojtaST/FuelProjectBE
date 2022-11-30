using FuelProject.Repositories;

namespace FuelProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CheckCredentials(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user is not null && user.Password == password) return true;
            return false;
        }
    }
}

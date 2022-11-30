using FuelProject.Domain.Entities;
using FuelProject.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FuelProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DiaryDbContext _context;

        public UserRepository(DiaryDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(string Id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == Id);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}

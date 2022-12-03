using FuelProject.Domain.DTos;
using FuelProject.Domain.Entities;
using FuelProject.Infrastructure;
using FuelProject.Services;
using MediatR;

namespace FuelProject.Users.Commands
{
    public class RegisterUserCommand : IRequest<LoginUserDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }

    public class RegisterUserCommnadHandler : IRequestHandler<RegisterUserCommand, LoginUserDto>
    {
        private readonly DiaryDbContext _context;
        private readonly IUserService _userService;

        public RegisterUserCommnadHandler(DiaryDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<LoginUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            //TODO vytvořit servicu
            User user = new User()
            {
                FirstName = request.FirstName,
                Surname = request.Surname,
                Password = request.Password,
                UserName = request.Username,
                Cars = new List<Car>()
            };

            _context.Add(user);
            await _context.SaveChangesAsync();
            var token = await _userService.GenerateToken();
            return new LoginUserDto(user.Id.ToString(), token);
        }
    }
}

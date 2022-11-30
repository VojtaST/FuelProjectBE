using FuelProject.Domain.Entities;
using FuelProject.Infrastructure;
using MediatR;

namespace FuelProject.Users.Commands
{
    public class RegisterUserCommand : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }

    public class RegisterUserCommnadHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly DiaryDbContext _context;

        public RegisterUserCommnadHandler(DiaryDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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
            return Unit.Value;
        }
    }
}

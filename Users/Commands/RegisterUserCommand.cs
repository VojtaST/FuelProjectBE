using FuelProject.Domain.DTos;
using FuelProject.Infrastructure;
using FuelProject.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Users.Commands
{
    public class RegisterUserCommand : IRequest<ActionResult<LoginUserDto>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }

    public class RegisterUserCommnadHandler : IRequestHandler<RegisterUserCommand, ActionResult<LoginUserDto>>
    {
        private readonly DiaryDbContext _context;
        private readonly IUserService _userService;

        public RegisterUserCommnadHandler(DiaryDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<ActionResult<LoginUserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            //TODO vytvořit servicu
            var user = await _userService.CreateUser(request.FirstName, request.Surname, request.Password, request.Username);
            _context.Add(user);
            await _context.SaveChangesAsync();
            var token = await _userService.GenerateToken();
            return new OkObjectResult(new LoginUserDto(user.Id.ToString(), token));
        }
    }
}

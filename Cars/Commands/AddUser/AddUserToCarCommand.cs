using FuelProject.Infrastructure;
using FuelProject.Repositories;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace FuelProject.Cars.Commands.AddUser;

[MediatRBehavior(typeof(ValidateAddUserToCarBehavior))]
public class AddUserToCarCommand : IRequest<ActionResult>
{
    public string UserId { get; set; }
    public string CarId { get; set; } = string.Empty;
}

public class AddUserToCarCommandHandler : IRequestHandler<AddUserToCarCommand, ActionResult>
{
    private readonly ICarRepository _carRepository;
    private readonly IUserRepository _userRepository;
    private readonly DiaryDbContext _diaryDbContext;

    public AddUserToCarCommandHandler(ICarRepository carRepository, IUserRepository userRepository, DiaryDbContext diaryDbContext)
    {
        _carRepository = carRepository;
        _userRepository = userRepository;
        _diaryDbContext = diaryDbContext;
    }

    public async Task<ActionResult> Handle(AddUserToCarCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);

        var car = await _carRepository.GetCarById(request.CarId);
        car.Users.Add(user);

        await _diaryDbContext.SaveChangesAsync();
        return new OkResult();
    }
}

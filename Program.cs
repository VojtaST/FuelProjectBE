using FuelProject.Infrastructure;
using FuelProject.Repositories;
using FuelProject.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DiaryDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("FuelProject"),
            new MariaDbServerVersion(new Version(10, 9, 3)));
});

// Add services to the container.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IFuelRecordRepository, FuelRecordRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFuelRecordService, FuelRecordService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = ConfigurationManagerCustom.AppSetting["JWT:ValidIssuer"],
            ValidAudience = ConfigurationManagerCustom.AppSetting["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManagerCustom.AppSetting["JWT:Secret"]))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(c =>
{
    //c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
    c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:53135",
                                    "http://localhost:4200"
                                    )
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowOrigin");



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

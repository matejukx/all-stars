using Serilog;
using AllStars.API.Endpoints;
using AllStars.Infrastructure.Dutch.Repository;
using AllStars.Domain.Dutch.Interfaces;
using AllStars.Infrastructure.User.Repository;
using AutoMapper;
using AllStars.API.Profiles;
using AllStars.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AllStars.API.DTO.Dutch;
using FluentValidation;
using AllStars.API.Validators;
using AllStars.Domain.Logs.Interfaces;
using AllStars.Application.Services.Dutch;
using AllStars.Domain.User.Interfaces;
using AllStars.Application.Options;
using AllStars.API.Extensions;
using AllStars.Application.Services.User;
using AllStars.API.DTO.User;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration
    .AddJsonFile("./appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// Register logger
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) 
    .WriteTo.Console()  
    .CreateLogger();
builder.Host.UseSerilog();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Register options
builder.Services
    .Configure<AllStarsIdentityOptions>(configuration.GetSection(nameof(AllStarsIdentityOptions)));

// Register Validators
builder.Services
    .AddTransient<IValidator<CreateDutchGameRequest>, CreateDutchGameRequestValidator>()
    .AddTransient<IValidator<PutScoreRequest>, PutScoreRequestValidator>()
    .AddTransient<IValidator<CreateUserRequest>, CreateUserRequestValidator>();

// Register services
builder.Services
    .AddScoped<IDutchService, DutchService>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<IUserService, UserService>();

// Register repositories
builder.Services
    .AddScoped<IDutchRepository, DutchRepository>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<ILogRepository, LogRepository>();

// Register db context
builder.Services.AddDbContext<AppDbContext>(
    options =>
        options.UseNpgsql(
            configuration.GetSection("PostgresDatabaseOptions:ConnectionString").Value),
    ServiceLifetime.Transient);

// Register mappers
var mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile(new CreateDutchGameCommandProfile());
    config.AddProfile(new DutchScoreResponseProfile());
    config.AddProfile(new CreateUserRequestProfile());
});
builder.Services.
    AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddJwtAuthentication(configuration);
builder.Services.AddAuthorization();

builder.Services.AddSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/dutch/all", DutchEndpoints.GetAll);
app.MapGet("/dutch", DutchEndpoints.GetUserScores);
app.MapPost("/dutch", DutchEndpoints.PostGame)
    .RequireAuthorization();
app.MapPut("/dutch", DutchEndpoints.PutScore)
    .RequireAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapPut("/dev/users", UserEndpoints.RegisterUser);
}

app.MapPost("/login", AuthEndpoints.Login);

app.Run();

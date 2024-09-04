using Serilog;
using AllStars.API.Endpoints;
using AllStars.Infrastructure.Dutch.Repository;
using AllStars.Domain.Dutch.Interfaces;
using AllStars.Infrastructure.User.Repository;
using AllStars.Application.Services;
using AutoMapper;
using AllStars.API.Profiles;
using AllStars.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration
    .AddJsonFile("./appsettings.json")
    .AddEnvironmentVariables()
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) 
    .WriteTo.Console()  
    .CreateLogger();

builder.Host.UseSerilog();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services
    .AddScoped<IDutchService, DutchService>();

builder.Services
    .AddScoped<IDutchRepository, DutchRepository>()
    .AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDbContext<AppDbContext>(
    options =>
        options.UseNpgsql(
            configuration.GetSection("PostgresDatabaseOptions:ConnectionString").Value),
    ServiceLifetime.Transient);

var mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile(new CreateDutchGameCommandProfile());
    config.AddProfile(new DutchScoreResponseProfile());
});

builder.Services.
    AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/dutch/all", DutchEndpoints.GetAll);
app.MapGet("/dutch", DutchEndpoints.GetUserScores);
app.MapPost("/dutch", DutchEndpoints.PostGame);
app.MapPut("/dutch", DutchEndpoints.PutScore);
app.MapPut("/dev/users", DutchEndpoints.AddUser);

app.Run();

//builder.Services
//    .Configure<MongoRepositoryOptions>(configuration.GetSection(nameof(MongoRepositoryOptions)))
//    .Configure<StationCacheOptions>(configuration.GetSection(nameof(StationCacheOptions)));


// builder.Services
//     .AddTransient<IValidator<StationSearchQuery>, StationsSearchQueryValidator>();




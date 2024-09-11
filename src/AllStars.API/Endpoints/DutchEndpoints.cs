using AllStars.API.DTO.Dutch;
using AllStars.Domain.Dutch.Interfaces;
using AllStars.Domain.Dutch.Models.Commands;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AllStars.API.Endpoints;

public static class DutchEndpoints
{
    public static async Task<IResult> PutScore(
        PutScoreRequest request,
        [FromServices] IDutchService dutchService,
        [FromServices] ILoggerFactory loggerFactory,
        [FromServices] IValidator<PutScoreRequest> validator,
        CancellationToken token)
    {
        var logger = loggerFactory.CreateLogger("UserEndpoints");
        try
        {
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var result = await dutchService.UpdateOne(request.GameId, request.NickName, request.Points, token);
            if (result is false)
            {
                return Results.NotFound("Something went wrong when updating DutchScore.");
            }

            return Results.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong when using PutScore endpoint: {ex}", ex);
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> PostGame(
        CreateDutchGameRequest request, 
        [FromServices] IMapper mapper,
        [FromServices] IDutchService dutchService,
        [FromServices] ILoggerFactory loggerFactory,
        [FromServices] IValidator<CreateDutchGameRequest> validator,
        CancellationToken token)
    {
        var logger = loggerFactory.CreateLogger("UserEndpoints");
        try
        {
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var command = mapper.Map<CreateDutchGameCommand>(request);
            await dutchService.CreateMany(command, token);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong when using PostGame endpoint: {ex}", ex);
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> GetUserScores(
        [AsParameters] GetUserScoresQuery query,
        [FromServices] IMapper mapper,
        [FromServices] IDutchService dutchService,
        [FromServices] ILoggerFactory loggerFactory, 
        CancellationToken token)
    {
        var logger = loggerFactory.CreateLogger("UserEndpoints");
        try
        {
            var results = await dutchService.GetUserScores(query.NickName, token);
            if (results is null)
            {
                return Results.NotFound();
            }

            var response = mapper.Map<List<DutchScoreResponse>>(results);
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong when using GetUserScore endpoint: {ex}", ex);
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> GetAll(
        [FromServices] IMapper mapper,
        [FromServices] IDutchService dutchService,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken token)
    {
        var logger = loggerFactory.CreateLogger("UserEndpoints");
        try
        {
            var results = await dutchService.GetAll(token);
            var response = mapper.Map<List<DutchScoreResponse>>(results);
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong when using GetAll endpoint: {ex}", ex);
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> AddUser(
        [FromServices] IUserRepository userRepository,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken token)
    {
        var logger = loggerFactory.CreateLogger("UserEndpoints");
        try
        {
            await userRepository.CreateDefaultUsers(token);

            logger.LogInformation("Inserted mocked users into database.");
            return Results.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong when using AddUser endpoint: {ex}", ex);
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

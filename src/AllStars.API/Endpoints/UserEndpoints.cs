using AllStars.API.DTO.Dutch;
using AllStars.API.DTO.User;
using AllStars.Domain.Dutch.Interfaces;
using AllStars.Domain.User.Interfaces;
using AllStars.Domain.User.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AllStars.API.Endpoints;

public static class UserEndpoints
{
    public static async Task<IResult> RegisterUser(
        CreateUserRequest request,
        [FromServices] IUserService userService,
        [FromServices] ILoggerFactory loggerFactory,
        [FromServices] IValidator<CreateUserRequest> validator,
        [FromServices] IMapper mapper,
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

            var user = mapper.Map<AllStarUser>(request);

            await userService.RegisterUserAsync(user, request.Password, token);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong when using AddUser endpoint: {ex}", ex);
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

using AllStars.API.DTO.User;
using AllStars.Domain.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AllStars.API.Endpoints;

public static class AuthEndpoints
{
    public static async Task<IResult> Login(
        UserLogin request, 
        [FromServices] IAuthService authService,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        var logger = loggerFactory.CreateLogger("AuthEndpoints");
        try
        {
            var validationStatus = await authService.ValidateUserAsync(request.Username, request.Password, cancellationToken);

            if (validationStatus)
            {
                var token = authService.GenerateJwtToken(request.Username);
                return Results.Ok(new { token });
            }

            return Results.Unauthorized();
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong when using Login endpoint: {ex}", ex);
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

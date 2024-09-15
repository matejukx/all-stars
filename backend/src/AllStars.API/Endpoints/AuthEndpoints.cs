using AllStars.API.DTO.User;
using AllStars.Domain.User.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ILogger = Serilog.ILogger;

namespace AllStars.API.Endpoints;

public static class AuthEndpoints
{
    static readonly ILogger _logger = Log.ForContext(typeof(AuthEndpoints));

    public static async Task<IResult> Login(
        UserLogin request, 
        [FromServices] IAuthService authService,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
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
            _logger.Error(ex, "Something went wrong when using Login endpoint.");
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

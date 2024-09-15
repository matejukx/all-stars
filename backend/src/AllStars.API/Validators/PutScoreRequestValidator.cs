using AllStars.API.DTO.Dutch;
using AllStars.Application.Helpers;
using FluentValidation;

namespace AllStars.API.Validators;

public class PutScoreRequestValidator : AbstractValidator<PutScoreRequest>
{
    public PutScoreRequestValidator()
    {
        RuleFor(x => x.Points)
            .InclusiveBetween(DutchHelpers.MinDutchPointsValue, DutchHelpers.MaxDutchPointsValue)
            .WithMessage("Score must be between 0 and 1000.");
    }
}

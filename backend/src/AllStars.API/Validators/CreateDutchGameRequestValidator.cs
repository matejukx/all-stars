using AllStars.API.DTO.Dutch;
using AllStars.Application.Helpers;
using FluentValidation;

namespace AllStars.API.Validators;

public class CreateDutchGameRequestValidator : AbstractValidator<CreateDutchGameRequest>
{
    public CreateDutchGameRequestValidator()
    {
        RuleFor(request => request.ScorePairs)
            .NotEmpty().WithMessage("ScorePairs must not be empty.")
            .ForEach(scorePair =>
            {
                scorePair.ChildRules(pair =>
                {
                    pair.RuleFor(x => x.Score)
                         .InclusiveBetween(DutchHelpers.MIN_DUTCH_POINTS_VALUE, DutchHelpers.MAX_DUTCH_POINTS_VALUE)
                         .WithMessage("Score must be between 0 and 1000.");
                });
            });
    }
}

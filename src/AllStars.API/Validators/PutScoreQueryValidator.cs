using AllStars.API.DTO.Dutch;
using FluentValidation;

namespace AllStars.API.Validators;

public class StationsSearchQueryValidator : AbstractValidator<PutScoreRequest>
{
    public StationsSearchQueryValidator()
    {
        RuleFor(x => x.Input)
             .Matches(@"^[a-zA-Z\s]*$").WithMessage("Input field can only contain letters and spaces.");
    }
}

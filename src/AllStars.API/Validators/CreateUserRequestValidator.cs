using AllStars.API.DTO.User;
using FluentValidation;

namespace AllStars.API.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        // Add login account validation rules + families field (maybe more?)
        RuleFor(request => request.NickName)
            .NotEmpty().WithMessage("Nickname must not be empty.");
    }
}
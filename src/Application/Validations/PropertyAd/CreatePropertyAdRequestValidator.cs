using Application.Dtos.PropertyAd;
using FluentValidation;


namespace Application.Validations.PropertyAd;

public class CreatePropertyAdRequestValidator:AbstractValidator<CreatePropertyAdRequest>    
{
    public CreatePropertyAdRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(220);
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2000);
        RuleFor(x => x.RoomCount)
            .GreaterThan(0)
            .LessThanOrEqualTo(50);
        RuleFor(x => x.Price)
            .GreaterThan(0);
        RuleFor(x => x.Area)
            .GreaterThan(0);

    }
}

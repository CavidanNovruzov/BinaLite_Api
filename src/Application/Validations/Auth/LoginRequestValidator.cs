
using Application.Dtos.Auth;
using FluentValidation;

namespace Application.Validations.Auth;

public class LoginRequestValidator:AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(l => l.Login).NotEmpty().MaximumLength(256);
        RuleFor(l=>l.Password).NotEmpty().MaximumLength(256);   
    }
}

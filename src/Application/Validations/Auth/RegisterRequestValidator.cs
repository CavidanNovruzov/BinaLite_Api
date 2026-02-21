using Application.Dtos.Auth;
using FluentValidation;

namespace Application.Validations.Auth;

public class RegisterRequestValidator: AbstractValidator<RegistrRequest>
{
    public RegisterRequestValidator()
    {
        // UserName
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("İstifadəçi adı boş ola bilməz.")
            .MinimumLength(3).WithMessage("İstifadəçi adı minimum 3 simvol olmalıdır.")
            .MaximumLength(50).WithMessage("İstifadəçi adı maksimum 50 simvol ola bilər.");

        // Email
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz.")
            .EmailAddress().WithMessage("Email formatı düzgün deyil.")
            .MaximumLength(256).WithMessage("Email maksimum 256 simvol ola bilər.");

        // Password
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə boş ola bilməz.")
            .MinimumLength(8).WithMessage("Şifrə minimum 8 simvol olmalıdır.")
            .MaximumLength(100).WithMessage("Şifrə maksimum 100 simvol ola bilər.")
            .Matches("[0-9]").WithMessage("Şifrə ən azı 1 rəqəm içərməlidir.")
            .Matches("[A-Z]").WithMessage("Şifrə ən azı 1 böyük hərf içərməlidir.")
            .Matches("[a-z]").WithMessage("Şifrə ən azı 1 kiçik hərf içərməlidir.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Şifrə ən azı 1 xüsusi simvol içərməlidir.");

        // FullName
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ad Soyad boş ola bilməz.")
            .MaximumLength(200).WithMessage("Ad Soyad maksimum 200 simvol ola bilər.");
    }
}

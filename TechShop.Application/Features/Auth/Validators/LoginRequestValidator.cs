using FluentValidation;
using TechShop.Application.Features.Auth.DTOs;

namespace TechShop.Application.Features.Auth.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x=> x.Email)
            .NotEmpty().WithMessage("El correo es obligatorio")
            .EmailAddress().WithMessage("El correo no es válido");

        RuleFor(x=> x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria")
            .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
            .Matches("[A-Z]").WithMessage("Debe tener al menos una mayúscula")
            .Matches("[a-z]").WithMessage("Debe tener al menos una minúscula")
            .Matches("[0-9]").WithMessage("Debe tener al menos un número");
    }
}
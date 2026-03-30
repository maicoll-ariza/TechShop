using FluentValidation;
using TechShop.Application.Features.Auth.DTOs;

namespace TechShop.Application.Features.Auth.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        // FirstName
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(50).WithMessage("El nombre no puede tener más de 50 caracteres");

        // LastName
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El apellido es obligatorio")
            .MaximumLength(50).WithMessage("El apellido no puede tener más de 50 caracteres");

        // Email
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo es obligatorio")
            .EmailAddress().WithMessage("El correo no es válido");

        // Password
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria")
            .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
            .Matches("[A-Z]").WithMessage("Debe tener al menos una mayúscula")
            .Matches("[a-z]").WithMessage("Debe tener al menos una minúscula")
            .Matches("[0-9]").WithMessage("Debe tener al menos un número");

        // PhoneNumber
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("El número de teléfono es obligatorio")
            .Matches(@"^\d{10}$").WithMessage("El número de teléfono debe tener 10 dígitos");
    }
}
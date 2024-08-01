using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace HealthMed.Application.Doctors.CreateDoctor;

[ExcludeFromCodeCoverage]
public sealed class CreateDoctorValidation : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorValidation()
    {
        RuleFor(p => p.Name).Length(3, 120).WithMessage("O nome deve ter entre 3 e 120 caracteres");
        RuleFor(p => p.Email).Length(3, 120).WithMessage("O e-mail deve ter entre 3 e 120 caracteres");
        RuleFor(p => p.CPF).NotEmpty().WithMessage("O CPF deve ser informado");
        RuleFor(p => p.CRM).NotEmpty().WithMessage("O CRM deve ser informado");
        RuleFor(p => p.Password).Length(8, 30).WithMessage("A senha deve ter entre 8 e 30 caracteres");
    }
}

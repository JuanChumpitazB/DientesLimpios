using FluentValidation;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente
{
    public class ValidadorComandoCrearPaciente: AbstractValidator<ComandoCrearPaciente>
    {
        public ValidadorComandoCrearPaciente()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .MaximumLength(250).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .MaximumLength(250).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}.")
                .EmailAddress().WithMessage("El formato del email no es válido.");

        }
    }
}

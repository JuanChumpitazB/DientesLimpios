using FluentValidation;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio
{
    public class ValidadorComandoActualizarConsultorio: AbstractValidator<ComandoActualizarConsultorio>
    {
        public ValidadorComandoActualizarConsultorio()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El campo {PropertyName} es obligatorio.")
                .MaximumLength(150).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}.");
        }
    }
}

using FluentValidation;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class ValidadorComandoCrearConsultorio : AbstractValidator<ComandoCrearConsultorio>
    {
        public ValidadorComandoCrearConsultorio()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del {PropertyName} es requerido.")
                .MaximumLength(150).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}.");

        }
    }
}

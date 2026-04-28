using FluentValidation;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class ValidadorComandoCrearConsultorio : AbstractValidator<ComandoCrearConsultorio>
    {
        public ValidadorComandoCrearConsultorio()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del {PropertyName} es requerido.");

        }
    }
}

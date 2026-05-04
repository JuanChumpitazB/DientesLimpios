using DientesLimpios.Dominio.Entidades;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerDetallePaciente
{
    public static class MapeadorExtensions
    {
        public static PacienteDetalleDto ADto(this Paciente paciente)
        {
            return new PacienteDetalleDto
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Email = paciente.Email.Valor
            };
        }
    }
}

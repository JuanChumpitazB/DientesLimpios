using DientesLimpios.Dominio.Entidades;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    public static class MapeadorExtensions
    {
        public static PacienteListadoDto ADto(this Paciente paciente)
        {
            return new PacienteListadoDto
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Email = paciente.Email.Valor
            };
        }

    }
}

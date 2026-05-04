namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    public class PacienteListadoDto
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Email { get; set; }
    }
}

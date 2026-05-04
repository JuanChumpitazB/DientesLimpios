namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerDetallePaciente
{
    public class PacienteDetalleDto
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Email { get; set; }
    }
}

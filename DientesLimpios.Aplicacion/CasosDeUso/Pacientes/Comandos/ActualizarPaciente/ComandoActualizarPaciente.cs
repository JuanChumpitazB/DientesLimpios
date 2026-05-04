using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente
{
    public class ComandoActualizarPaciente: IRequest
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Email { get; set; }




    }
}

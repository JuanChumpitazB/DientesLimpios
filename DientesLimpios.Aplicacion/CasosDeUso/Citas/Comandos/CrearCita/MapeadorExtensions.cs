using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using DientesLimpios.Dominio.Entidades;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CrearCita
{
    public static class MapeadorExtensions
    {
        public static ConfirmacionCitaDto ADto(this Cita cita)
        {
            return new ConfirmacionCitaDto
            {
                Id = cita.Id,
                Fecha = cita.IntervaloDeTiempo.Inicio,
                Paciente = cita.Paciente!.Nombre,
                Paciente_Email = cita.Paciente.Email.Valor,
                Dentista = cita.Dentista!.Nombre,
                Consultorio = cita.Consultorio!.Nombre
            };
        }
    }
}

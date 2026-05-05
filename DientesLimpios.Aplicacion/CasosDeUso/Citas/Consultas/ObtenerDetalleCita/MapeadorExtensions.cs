using DientesLimpios.Dominio.Entidades;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerDetalleCita
{
    public static class MapeadorExtensions
    {
        public static CitaDetalleDto ADto(this Cita cita)
        {
            return new CitaDetalleDto
            {
                Id = cita.Id,
                Paciente = cita.Paciente!.Nombre ?? "N/A",
                Dentista = cita.Dentista!.Nombre ?? "N/A",
                Consultorio = cita.Consultorio!.Nombre ?? "N/A",
                FechaInicio = cita.IntervaloDeTiempo.Inicio,
                FechaFin = cita.IntervaloDeTiempo.Fin,
                EstadoCita = cita.Estado.ToString()
            };
        }
    }
}

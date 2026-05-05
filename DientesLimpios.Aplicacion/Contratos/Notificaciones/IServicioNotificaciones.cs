namespace DientesLimpios.Aplicacion.Contratos.Notificaciones
{
    public interface IServicioNotificaciones
    {
        Task EnviarConfirmacionCita(ConfirmacionCitaDto cita);
        Task EnviarRecordatorioCita(RecordatorioCitaDto cita);
    }
}

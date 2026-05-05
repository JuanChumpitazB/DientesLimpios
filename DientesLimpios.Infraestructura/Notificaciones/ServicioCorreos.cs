using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Mail;

namespace DientesLimpios.Infraestructura.Notificaciones
{
    public class ServicioCorreos : IServicioNotificaciones
    {
        private readonly IConfiguration configuration;

        public ServicioCorreos(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task EnviarConfirmacionCita(ConfirmacionCitaDto cita)
        {
            var asunto = "Confirmación de cita - Dientes Limpios.";
            var cuerpo = $"""
                Estimado(a) {cita.Paciente},
                Su cita con el Dr.(a) {cita.Dentista} ha sico programada para el {cita.Fecha.ToString("f", new CultureInfo("es-DO"))} en el consultorio {cita.Consultorio}.

                ¡Lo(a) esperamos!

                Equipo de Dientes Limpios.
                """;

            await EnviarMensaje(cita.Paciente_Email, asunto, cuerpo);
        }

        public async Task EnviarRecordatorioCita(RecordatorioCitaDto cita)
        {
            var asunto = "Recordatorio de cita - Dientes Limpios.";
            var cuerpo = $"""
                Estimado(a) {cita.Paciente},
                Le recordamos que tiene una cita con el Dr.(a) {cita.Dentista} programada para el {cita.Fecha.ToString("f", new CultureInfo("es-DO"))} en el consultorio {cita.Consultorio}.
                ¡Lo(a) esperamos!
                Equipo de Dientes Limpios.
                """;

            await EnviarMensaje(cita.Paciente_Email, asunto, cuerpo);
        }

        private async Task EnviarMensaje(string mailDestinatario, string asunto, string cuerpo)
        {
            var nuestroEmail = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:EMAIL");
            var password = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:PASSWORD");
            var host = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:HOST");
            var puerto = configuration.GetValue<int>("CONFIGURACIONES_EMAIL:PUERTO");

            var smtpCliente = new SmtpClient(host,puerto);
            smtpCliente.EnableSsl = true;
            smtpCliente.UseDefaultCredentials = false;
            smtpCliente.Credentials = new NetworkCredential(nuestroEmail, password);

            var mensaje = new MailMessage(nuestroEmail!, mailDestinatario, asunto, cuerpo);
            await smtpCliente.SendMailAsync(mensaje);
        }
    }
}

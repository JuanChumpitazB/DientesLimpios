using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using DientesLimpios.Infraestructura.Notificaciones;
using Microsoft.Extensions.DependencyInjection;

namespace DientesLimpios.Infraestructura
{
    public static class RegistroDeServiciosDeInfraestructura
    {
        public static IServiceCollection AgregarServiciosDeInfraestructura(this IServiceCollection servicios)
        {
            servicios.AddScoped<IServicioNotificaciones, ServicioCorreos>();
            return servicios;
        }
    }
}

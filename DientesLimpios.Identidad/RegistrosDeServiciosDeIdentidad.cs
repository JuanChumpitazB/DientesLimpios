using DientesLimpios.Aplicacion.Contratos.Identidad;
using DientesLimpios.Identidad.Modelos;
using DientesLimpios.Identidad.Servicios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DientesLimpios.Identidad
{
    public static class RegistrosDeServiciosDeIdentidad
    {
        public static void AgregarServiciosDeIdentidad(this IServiceCollection services)
        {
            services.AddAuthentication(IdentityConstants.BearerScheme).AddBearerToken(IdentityConstants.BearerScheme);
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("esadmin", politica => politica.RequireClaim("esadmin"));
            });

            services.AddDbContext<DientesLimpiosIdentityDbContext>(options => 
                                            options.UseSqlServer("name=DientesLimpiosConnectionString"));
            services.AddIdentityCore<Usuario>()
                .AddEntityFrameworkStores<DientesLimpiosIdentityDbContext>()
                .AddApiEndpoints();

            services.AddTransient<IServicioUsuarios, ServicioUsuarios>();
            services.AddHttpContextAccessor();
        }
    }
}

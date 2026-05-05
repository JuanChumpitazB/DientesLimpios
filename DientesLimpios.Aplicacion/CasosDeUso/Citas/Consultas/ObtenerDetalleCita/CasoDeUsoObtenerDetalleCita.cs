using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerDetalleCita
{
    public class CasoDeUsoObtenerDetalleCita : IRequestHandler<ConsultaObtenerDetalleCita, CitaDetalleDto>
    {
        private readonly IRepositorioCitas repositorio;

        public CasoDeUsoObtenerDetalleCita(IRepositorioCitas repositorio)
        {
            this.repositorio = repositorio;
        }
        public async Task<CitaDetalleDto> Handle(ConsultaObtenerDetalleCita request)
        {
            var cita = await repositorio.ObtenerPorId(request.Id);
            if (cita is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            return cita.ADto();
        }
    }
}

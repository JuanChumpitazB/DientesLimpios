using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerListadoCitas
{
    public class CasoDeUsoObtenerListadoCitas : IRequestHandler<ConsultaObtenerListadoCitas, List<CitaListadoDto>>
    {
        private readonly IRepositorioCitas repositorio;

        public CasoDeUsoObtenerListadoCitas(IRepositorioCitas repositorio)
        {
            this.repositorio = repositorio;
        }
        public async Task<List<CitaListadoDto>> Handle(ConsultaObtenerListadoCitas request)
        {
            var citas = await repositorio.ObtenerFiltrado(request);
            var citasDto = citas.Select(cita => cita.ADto()).ToList();
            return citasDto;
        }
    }
}

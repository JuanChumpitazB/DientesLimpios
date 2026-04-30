using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios
{
    public class CasoDeUsoObtenerListadoConsultorios : IRequestHandler<ConsultaObtenerListadoConsultorios, List<ConsultorioListadoDto>>
    {
        private readonly IRepositorioConsultorios repositorio;

        public CasoDeUsoObtenerListadoConsultorios(IRepositorioConsultorios repositorio)
        {
            this.repositorio = repositorio;
        }
        public async Task<List<ConsultorioListadoDto>> Handle(ConsultaObtenerListadoConsultorios request)
        {
            var consultorios = await repositorio.ObtenerTodos(); 
            var consultoriosDto = consultorios.Select(consultorio => consultorio.ADto()).ToList();

            return consultoriosDto;
        }
    }
}

using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    public class CasoDeUsoObtenerListadoPacientes : IRequestHandler<ConsultaObtenerListadoPacientes, PaginadoDto<PacienteListadoDto>>
    {
        private readonly IRepositorioPacientes repositorio;

        public CasoDeUsoObtenerListadoPacientes(IRepositorioPacientes repositorio)
        {
            this.repositorio = repositorio;
        }
        public async Task<PaginadoDto<PacienteListadoDto>> Handle(ConsultaObtenerListadoPacientes request)
        {
            var pacientes = await repositorio.ObtenerPorFiltro(request);
            var totalPacientes = await repositorio.ObtenerCantidadTotalRegistros();
            var pacientesDto = pacientes.Select(paciente => paciente.ADto()).ToList();

            var paginadoDto = new PaginadoDto<PacienteListadoDto>
            {
                Elementos = pacientesDto,
                Total = totalPacientes
            };

            return paginadoDto;
        }
    }
}

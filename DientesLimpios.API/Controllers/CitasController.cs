using DientesLimpios.API.DTOs.Citas;
using DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CancelarCita;
using DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CompletarCita;
using DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CrearCita;
using DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerDetalleCita;
using DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerListadoCitas;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/citas")]
    public class CitasController : ControllerBase
    {
        private readonly IMediator mediator;

        public CitasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CitaListadoDto>>> Get([FromQuery] ConsultaObtenerListadoCitas consulta) 
        { 
            var resultado = await mediator.Send(consulta);
            return resultado;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CitaDetalleDto>> Get(Guid id)
        {
            var consulta = new ConsultaObtenerDetalleCita { Id = id };
            var resultado = await mediator.Send(consulta);
            return resultado;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearCitaDto crearCitaDto)
        {
            var comando = new ComandoCrearCita
            {
                ConsultorioId = crearCitaDto.ConsultorioId,
                DentistaId = crearCitaDto.DentistaId,
                PacienteId = crearCitaDto.PacienteId,
                FechaInicio = crearCitaDto.FechaInicio,
                FechaFin = crearCitaDto.FechaFin
            };
            var resultado = await mediator.Send(comando);
            return Ok();
        }

        [HttpPost("{id}/completar")]
        public async Task<IActionResult> Completar(Guid id)
        {
            var comando = new ComandoCompletarCita { Id = id };
            await mediator.Send(comando);
            return Ok();
        }
        [HttpPost("{id}/cancelar")]
        public async Task<IActionResult> Cancelar(Guid id)
        {
            var comando = new ComandoCancelarCita { Id = id };
            await mediator.Send(comando);
            return Ok();
        }
    }
}

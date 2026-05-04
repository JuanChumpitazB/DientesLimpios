using DientesLimpios.API.DTOs.Citas;
using DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CrearCita;
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
    }
}

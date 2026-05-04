using DientesLimpios.API.DTOs.Pacientes;
using DientesLimpios.API.Utilidades;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.BorrarPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerDetallePaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/pacientes")]
    public class PacientesController : ControllerBase
    {
        private readonly IMediator mediator;

        public PacientesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearPacienteDto crearPacienteDto)
        {
            var comando = new ComandoCrearPaciente
            {
                Nombre = crearPacienteDto.Nombre,
                Email = crearPacienteDto.Email
            };
            await mediator.Send(comando);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<PacienteListadoDto>>> Get([FromQuery] ConsultaObtenerListadoPacientes consulta)
        {
            var pacientes = await mediator.Send(consulta);
            HttpContext.InsertarPaginacionEnCabecera(pacientes.Total);
            return pacientes.Elementos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDetalleDto>> Get(Guid id)
        {
            var consulta = new ConsultaObtenerDetallePaciente() { Id = id };
            var paciente = await mediator.Send(consulta);
            return paciente;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActualizarPacienteDto actualizarPacienteDto)
        {
            var comando = new ComandoActualizarPaciente
            {
                Id = id,
                Nombre = actualizarPacienteDto.Nombre,
                Email = actualizarPacienteDto.Email
            };
            await mediator.Send(comando);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var comando = new ComandoBorrarPaciente{ Id = id };
            await mediator.Send(comando);
            return Ok();
        }
    }
}

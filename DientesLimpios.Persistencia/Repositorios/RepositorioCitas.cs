using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios.Modelos;
using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class RepositorioCitas : Repositorio<Cita>, IRepositorioCitas
    {
        private readonly DientesLimpiosDbContext context;

        public RepositorioCitas(DientesLimpiosDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public async Task<bool> ExisteCitaSolapada(Guid dentistaId, DateTime inicio, DateTime fin)
        {
            return await context.Citas
                .Where(x => x.DentistaId == dentistaId
                    && x.Estado == Dominio.Enums.EstadoCita.Programada
                    && inicio < x.IntervaloDeTiempo.Fin
                    && fin > x.IntervaloDeTiempo.Inicio
                ).AnyAsync();
        }

        public async Task<IEnumerable<Cita>> ObtenerFiltrado(FiltroCitasDto filtroCitasDto)
        {
            var queryable = context.Citas
                .Include(x => x.Paciente)
                .Include(x => x.Dentista)
                .Include(x => x.Consultorio)
                .AsQueryable();
            if(filtroCitasDto.ConsultorioId is not null)
            {
                queryable = queryable.Where(x => x.ConsultorioId == filtroCitasDto.ConsultorioId);
            }
            if(filtroCitasDto.DentistaId is not null)
            {
                queryable = queryable.Where(x => x.DentistaId == filtroCitasDto.DentistaId);
            }
            if(filtroCitasDto.PacienteId is not null)
            {
                queryable = queryable.Where(x => x.PacienteId == filtroCitasDto.PacienteId);
            }

            if(filtroCitasDto.EstadoCita is not null)
            {
                queryable = queryable.Where(x => x.Estado == filtroCitasDto.EstadoCita);
            }

            return await queryable.Where(x => x.IntervaloDeTiempo.Inicio >= filtroCitasDto.FechaInicio
                && x.IntervaloDeTiempo.Fin < filtroCitasDto.FechaFin)
                .OrderBy(x => x.IntervaloDeTiempo.Inicio)
                .ToListAsync();
        }

        new public async Task<Cita?> ObtenerPorId(Guid id)
        {
            return await context.Citas
                .Include(x => x.Paciente)
                .Include(x => x.Dentista)
                .Include(x => x.Consultorio)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

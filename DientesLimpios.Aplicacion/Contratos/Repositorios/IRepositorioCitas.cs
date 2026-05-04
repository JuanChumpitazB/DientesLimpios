using DientesLimpios.Dominio.Entidades;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    public interface IRepositorioCitas: IRepositorio<Cita>
    {
        Task<bool> ExisteCitaSolapada(Guid dentistaId, DateTime inicio, DateTime fin);
    }
}

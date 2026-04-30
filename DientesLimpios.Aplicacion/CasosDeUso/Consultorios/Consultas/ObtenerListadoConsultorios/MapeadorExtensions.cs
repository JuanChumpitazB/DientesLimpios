using DientesLimpios.Dominio.Entidades;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios
{
    public static class MapeadorExtensions
    {
        public static ConsultorioListadoDto ADto(this Consultorio consultorio)
        {
            var dto = new ConsultorioListadoDto
            {
                Id = consultorio.Id,
                Nombre = consultorio.Nombre
            };
            return dto;
        }
    }
}

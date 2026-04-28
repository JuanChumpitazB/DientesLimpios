using DientesLimpios.Dominio.Excepciones;

namespace DientesLimpios.Dominio.ObjetosDeValor
{
    public record IntervaloDeTiempo
    {
        public DateTime Inicio { get; }
        public DateTime Fin { get; }
        public IntervaloDeTiempo(DateTime inicio, DateTime fin)
        {
            if (inicio > fin)
            {
                throw new ExcepcionDeReglaDeNegocio($"La fecha de inicio no puede ser posterior a la fecha fin.");
            }

            Inicio = inicio;
            Fin = fin;
        }
    }
}

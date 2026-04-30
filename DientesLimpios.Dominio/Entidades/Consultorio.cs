using DientesLimpios.Dominio.Excepciones;

namespace DientesLimpios.Dominio.Entidades
{
    public class Consultorio
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;

        protected Consultorio()
        {
            // Requerido por EF Core
        }
        public Consultorio(string nombre)
        {
            AplicarReglasDeNegocio(nombre);

            Nombre = nombre;
            Id = Guid.CreateVersion7();
        }

        public void ActualizarNombre(string nombre)
        {
            AplicarReglasDeNegocio(nombre);
            Nombre = nombre;
        }

        private void AplicarReglasDeNegocio(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio.");
            }
        }
    }
}

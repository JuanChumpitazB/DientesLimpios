using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DientesLimpios.Dominio.Entidades
{
    public class Paciente
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;
        public Email Email { get; private set; } = null!;

        public Paciente(string nombre, Email email)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio.");
            }
            if (email is null)
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio.");
            }

            Nombre = nombre;
            Email = email;
            Id = Guid.CreateVersion7();
        }
    }
}

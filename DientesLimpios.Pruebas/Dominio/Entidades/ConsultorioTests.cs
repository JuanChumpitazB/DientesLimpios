using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    [TestClass]
    public class ConsultorioTests
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_NombreNullo_LanzaExcepcion()
        {
            new Consultorio(null!);
        }
    }
}

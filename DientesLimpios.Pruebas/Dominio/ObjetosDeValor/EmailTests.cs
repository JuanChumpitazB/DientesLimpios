using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;

namespace DientesLimpios.Pruebas.Dominio.ObjetosDeValor
{
    // Proyecto MSTest, por eso se usan los atributos [TestClass] y [TestMethod]
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            new Email(null!);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_EmailSinArroba_LanzaExcepcion()
        {
            new Email("felipe.com");
        }

        [TestMethod]
        public void Constructor_EmailEsValido_NoLanzaExcepcion()
        {
            new Email("felipe@ejemplo.com");
        }
    }
}

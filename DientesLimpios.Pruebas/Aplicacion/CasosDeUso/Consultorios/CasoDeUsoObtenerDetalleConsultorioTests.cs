using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoObtenerDetalleConsultorioTests
    {
        private IRepositorioConsultorios repositorio;
        private CasoDeUsoObtenerDetalleConsultorio casoDeUso;

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            casoDeUso = new CasoDeUsoObtenerDetalleConsultorio(repositorio);
        }

        [TestMethod]
        public async Task Handle_ConsultorioExiste_RetornaDto()
        {
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var consulta = new ConsultaObtenerDetalleConsultorio { Id = id };

            repositorio.ObtenerPorId(id).Returns(consultorio);

            // Prueba
            var resultado = await casoDeUso.Handle(consulta);

            // Verificacion
            Assert.IsNotNull(resultado);
            Assert.AreEqual(id, resultado.Id);
            Assert.AreEqual("Consultorio A", resultado.Nombre);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionNoEncontrado))]
        public async Task Handle_ConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            var id = Guid.NewGuid();
            var consulta = new ConsultaObtenerDetalleConsultorio { Id = id };

            repositorio.ObtenerPorId(id).ReturnsNull();

            // Prueba 
            await casoDeUso.Handle(consulta);
        }

    }
}

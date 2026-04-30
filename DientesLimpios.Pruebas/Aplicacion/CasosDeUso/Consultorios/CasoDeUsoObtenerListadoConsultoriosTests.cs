using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoObtenerListadoConsultoriosTests
    {
        private IRepositorioConsultorios repositorio;
        private CasoDeUsoObtenerListadoConsultorios casoDeUso;

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            casoDeUso = new CasoDeUsoObtenerListadoConsultorios(repositorio);
        }

        [TestMethod]
        public async Task Handle_CuandoHayConsultorios_RetornaListaDeConsultoListadoDto()
        {
            // Arrange
            var consultorios = new List<Consultorio>
            {
                new Consultorio ("Consultorio 1"),
                new Consultorio ("Consultorio 2")
            };
            repositorio.ObtenerTodos().Returns(consultorios);

            var esperado = consultorios.Select(c => new ConsultorioListadoDto
            {
                Id = c.Id,
                Nombre = c.Nombre
            }).ToList();

            // Act
            var resultado = await casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());
            // Assert
            Assert.AreEqual(esperado.Count, resultado.Count);

            for (int i = 0; i < esperado.Count; i++)
            {
                Assert.AreEqual(esperado[i].Id, resultado[i].Id);
                Assert.AreEqual(esperado[i].Nombre, resultado[i].Nombre);
            }
        }

        [TestMethod]
        public async Task Handle_CuandoNoHayConsultorios_RetornaListaVacia()
        {
            // Arrange
            repositorio.ObtenerTodos().Returns(new List<Consultorio>());
            // Act
            var resultado = await casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());
            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(0, resultado.Count);

        }
    }
}

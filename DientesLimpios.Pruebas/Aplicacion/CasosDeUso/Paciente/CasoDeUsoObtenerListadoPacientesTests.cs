using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.ObjetosDeValor;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Paciente
{
    [TestClass]
    public class CasoDeUsoObtenerListadoPacientesTests
    {
        private IRepositorioPacientes repositorio;
        private CasoDeUsoObtenerListadoPacientes casoDeUso;

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioPacientes>();
            casoDeUso = new CasoDeUsoObtenerListadoPacientes(repositorio);
        }

        [TestMethod]
        public async Task Handle_RetornaPacientesPaginadosCorrectamente()
        {
            var pagina = 1;
            var registrosPorPagina = 2;

            var filtroPacienteDto = new FiltroPacienteDto
            {
                Pagina = pagina,
                RegistrosPorPagina = registrosPorPagina
            };

            var paciente1 = new DientesLimpios.Dominio.Entidades.Paciente("Felipe", new Email("felipe@ejemplo.com"));
            var paciente2 = new DientesLimpios.Dominio.Entidades.Paciente("Claudia", new Email("claudia@ejejmplo.com"));

            IEnumerable<DientesLimpios.Dominio.Entidades.Paciente> pacientes = new List<DientesLimpios.Dominio.Entidades.Paciente>
            {
                paciente1,
                paciente2
            };

            repositorio.ObtenerPorFiltro(Arg.Any<FiltroPacienteDto>()).Returns(Task.FromResult(pacientes));

            repositorio.ObtenerCantidadTotalRegistros().Returns(Task.FromResult(10));

            var request = new ConsultaObtenerListadoPacientes
            {
                Pagina = pagina,
                RegistrosPorPagina = registrosPorPagina
            };

            var resultado = await casoDeUso.Handle(request);

            Assert.AreEqual(10, resultado.Total);
            Assert.AreEqual(2, resultado.Elementos.Count);
            Assert.AreEqual("Felipe", resultado.Elementos[0].Nombre);
            Assert.AreEqual("Claudia", resultado.Elementos[1].Nombre);

        }

        [TestMethod]
        public async Task Handle_CuandoNoHayPacientes_RetornaListaVaciaYTotalCero()
        {
            var pagina = 1;
            var registrosPorPagina = 5;

            var filtroPacienteDto = new FiltroPacienteDto
            {
                Pagina = pagina,
                RegistrosPorPagina = registrosPorPagina
            };

            IEnumerable<DientesLimpios.Dominio.Entidades.Paciente> pacientes = new List<DientesLimpios.Dominio.Entidades.Paciente>();

            repositorio.ObtenerPorFiltro(Arg.Any<FiltroPacienteDto>()).Returns(Task.FromResult(pacientes));

            repositorio.ObtenerCantidadTotalRegistros().Returns(Task.FromResult(0));

            var request = new ConsultaObtenerListadoPacientes
            {
                Pagina = pagina,
                RegistrosPorPagina = registrosPorPagina
            };
             
            var resultado = await casoDeUso.Handle(request);

            Assert.AreEqual(0, resultado.Total);
            Assert.IsNotNull(resultado.Elementos);
            Assert.AreEqual(0, resultado.Elementos.Count);

        }



    }
}

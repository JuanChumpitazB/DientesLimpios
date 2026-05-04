
using DientesLimpios.Dominio.Entidades; // Agrega esta línea para importar el tipo Paciente
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.ObjetosDeValor;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Paciente
{
    [TestClass]
    public class CasoDeUsoCrearPacienteTests
    {
        private IRepositorioPacientes repositorio;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CasoDeUsoCrearPaciente casoDeUso;

        [TestInitialize]
        public void Inicializar()
        {
            repositorio = Substitute.For<IRepositorioPacientes>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CasoDeUsoCrearPaciente(repositorio, unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoDatosValidos_CrearPacienteYPersisteYRetornaId()
        {
            var comando = new ComandoCrearPaciente
            {
                Nombre = "Felipe",
                Email = "felipe@ejemplo.com"
            };
            var pacienteCreado = new DientesLimpios.Dominio.Entidades.Paciente(comando.Nombre, new Email(comando.Email));
            var id = pacienteCreado.Id;

            repositorio.Agregar(Arg.Any<DientesLimpios.Dominio.Entidades.Paciente>()).Returns(pacienteCreado);
            var idResultado = await casoDeUso.Handle(comando);

            Assert.AreEqual(id, idResultado);
            await repositorio.Received(1).Agregar(Arg.Any<DientesLimpios.Dominio.Entidades.Paciente>());
            await unidadDeTrabajo.Received(1).Persistir();

        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcion_ReversarYLanzaExecpcion()
        {
            var comando = new ComandoCrearPaciente
            {
                Nombre = "Felipe",
                Email = "felipe@ejemplo.com"
            };
            repositorio.Agregar(Arg.Any<DientesLimpios.Dominio.Entidades.Paciente>())
                .Throws(new InvalidOperationException("Error al insertar."));

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => casoDeUso.Handle(comando));

            await unidadDeTrabajo.Received(1).Reversar();
            await unidadDeTrabajo.DidNotReceive().Persistir(); 

        }
    }
}

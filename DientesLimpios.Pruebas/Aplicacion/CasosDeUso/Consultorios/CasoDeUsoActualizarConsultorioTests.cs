using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoActualizarConsultorioTests
    {
        private IRepositorioConsultorios repositorio;
        private IUnidadDeTrabajo    unidadDeTrabajo;
        private CasoDeUsoActualizarConsultorio casoDeUso;

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CasoDeUsoActualizarConsultorio(repositorio, unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioExiste_ActualizarNombreYPersiste()
        {
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var comando = new ComandoActualizarConsultorio { Id=id, Nombre="Consultorio Actualizado" };

            repositorio.ObtenerPorId(id).Returns(consultorio);

            await casoDeUso.Handle(comando);

            await repositorio.Received(1).Actualizar(consultorio);
            await unidadDeTrabajo.Received(1).Persistir();

        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionNoEncontrado))]
        public async Task Handle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            var comando = new ComandoActualizarConsultorio { Id = Guid.NewGuid(), Nombre = "Nombre...." };
            repositorio.ObtenerPorId(comando.Id).ReturnsNull();

            await casoDeUso.Handle(comando);
             

        }

        [TestMethod]
        public async Task Handle_CuandoOcurreErrorAlActualizar_LlamaReversarYLanzaExcepcion() 
        { 
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var comando = new ComandoActualizarConsultorio { Id = id, Nombre = "Consultorio B" };

            repositorio.ObtenerPorId(id).Returns(consultorio);
            repositorio.Actualizar(consultorio).Throws(new InvalidOperationException("Error al actualizar"));

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await casoDeUso.Handle(comando));
            await unidadDeTrabajo.Received(1).Reversar();
        }

    }
}

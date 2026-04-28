using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoCrearConsultorioTests
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositorioConsultorios repositorio;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CasoDeUsoCrearConsultorio casoDeUso;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup() 
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CasoDeUsoCrearConsultorio(repositorio, unidadDeTrabajo); 
        }

        [TestMethod]
        public async Task Handle_ComandoValido_ObtenemosIdConsultorio() 
        {
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" };
            var consultorioCreado = new Consultorio("Consultorio A");
            repositorio.Agregar(Arg.Any<Consultorio>()).Returns(consultorioCreado);
            var resultado = await casoDeUso.Handle(comando);

            await repositorio.Received(1).Agregar(Arg.Any<Consultorio>());
            await unidadDeTrabajo.Received(1).Persistir();
            Assert.AreNotEqual(Guid.Empty, resultado);

        }
        
        [TestMethod]
        public async Task Handle_CuandoHayError_HacemosRollback()
        {
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" };
            repositorio.Agregar(Arg.Any<Consultorio>()).Throws<Exception>();

            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                var resultado = await casoDeUso.Handle(comando);
            });

            await unidadDeTrabajo.Received(1).Reversar(); // Verificar que se hizo rollback en caso de error

        }


    }
}

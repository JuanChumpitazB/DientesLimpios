
using DientesLimpios.Aplicacion.Excepciones;
using FluentValidation;
using FluentValidation.Results;

namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    public class MediadorSimple : IMediator
    {
        private readonly IServiceProvider serviceProvider;

        public MediadorSimple(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task<IResponse> Send<IResponse>(IRequest<IResponse> request)
        {
            var tipoValidador = typeof(IValidator<>).MakeGenericType(request.GetType());
            var validador = serviceProvider.GetService(tipoValidador);

            if (validador is not null)
            {
                var metodoValidar = tipoValidador.GetMethod("ValidateAsync");
                var tareaValidar = (Task)metodoValidar!.Invoke(validador, 
                    new object[] { request, CancellationToken.None })!;
                
                await tareaValidar.ConfigureAwait(false);

                var resultado = tareaValidar.GetType().GetProperty("Result");
                var validationResult = (ValidationResult)resultado!.GetValue(tareaValidar)!;

                if (!validationResult.IsValid)
                {
                    throw new ExcepcionDeValidacion(validationResult);
                }
            }

            var tipoCasoDeUso = typeof(IRequestHandler<,>)
                                .MakeGenericType(request.GetType(), typeof(IResponse));

            var casoDeUso = serviceProvider.GetService(tipoCasoDeUso);

            if(casoDeUso is null)
            {
                throw new ExcepcionDeMediador($"No se encontró un handler {request.GetType().Name}");
            }

            var metodo = tipoCasoDeUso.GetMethod("Handle");
            return await (Task<IResponse>)metodo.Invoke(casoDeUso, new object[] { request })!;

        }
    }
}

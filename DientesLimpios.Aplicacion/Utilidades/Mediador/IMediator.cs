namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    public interface IMediator
    {
        Task<IResponse> Send<IResponse>(IRequest<IResponse> request);
        Task Send(IRequest request);
    }
}

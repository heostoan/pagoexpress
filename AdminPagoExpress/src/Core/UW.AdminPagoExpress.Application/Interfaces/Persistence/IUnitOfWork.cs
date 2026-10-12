namespace UW.AdminPagoExpress.Application.Interfaces.Persistence;

public interface IUnitOfWork
{
    Task<int> GuardarCambiosAsync(CancellationToken cancellationToken = default);
}
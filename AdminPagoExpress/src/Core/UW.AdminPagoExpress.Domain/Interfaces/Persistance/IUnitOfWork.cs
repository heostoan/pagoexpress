using Oracle.ManagedDataAccess.Client;

namespace UW.AdminPagoExpress.Domain.Interfaces.Persistance;

public interface IUnitOfWork : IAsyncDisposable
{
    OracleConnection? Connection { get; }
    OracleTransaction? Transaction { get; }

    Task BeginAsync(CancellationToken ct = default);
    Task CommitAsync(CancellationToken ct = default);
    Task RollbackAsync(CancellationToken ct = default);
}

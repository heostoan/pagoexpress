using Oracle.ManagedDataAccess.Client;

namespace UW.AdminPagoExpress.Application.Interfaces.Persistence;

public interface IOracleTransactionManager : IAsyncDisposable
{
    OracleConnection? Connection { get; }
    OracleTransaction? Transaction { get; }

    Task BeginAsync(CancellationToken ct = default);
    Task CommitAsync(CancellationToken ct = default);
    Task RollbackAsync(CancellationToken ct = default);
}
using UW.AdminPagoExpress.Domain.Interfaces.Persistance;

namespace UW.AdminPagoExpress.Infraestructure.Persistence;

public sealed class NullUnitOfWork : IUnitOfWork
{
    public OracleConnection? Connection => null;
    public OracleTransaction? Transaction => null;

    public Task BeginAsync(CancellationToken ct = default) => Task.CompletedTask;
    public Task CommitAsync(CancellationToken ct = default) => Task.CompletedTask;
    public Task RollbackAsync(CancellationToken ct = default) => Task.CompletedTask;

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}

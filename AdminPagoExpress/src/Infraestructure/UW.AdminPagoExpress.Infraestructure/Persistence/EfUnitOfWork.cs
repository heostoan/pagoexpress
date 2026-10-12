using UW.AdminPagoExpress.Application.Interfaces.Persistence;

namespace UW.AdminPagoExpress.Infrastructure.Persistence;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public EfUnitOfWork(ApplicationDbContext context) => _context = context;

    public Task<int> GuardarCambiosAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}
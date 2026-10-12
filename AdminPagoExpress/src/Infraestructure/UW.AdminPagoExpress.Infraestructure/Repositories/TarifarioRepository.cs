using Microsoft.EntityFrameworkCore;
using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Infrastructure.Persistence;

namespace UW.AdminPagoExpress.Infrastructure.Repositories;

public class TarifarioRepository : ITarifarioRepository
{
    private readonly ApplicationDbContext _context;

    public TarifarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Tarifario?> ObtenerPorIdAsync(long id, CancellationToken cancellationToken)
        => await _context.Tarifarios.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<List<Tarifario>> ListarAsync(CancellationToken cancellationToken)
        => await _context.Tarifarios.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<bool> ExisteCodigoAsync(string codigoSpring, string codigoOracle, long? idExcluir, CancellationToken cancellationToken)
        => await _context.Tarifarios
            .AsNoTracking()
            .AnyAsync(t => (t.CodigoSpring == codigoSpring || t.CodigoOracle == codigoOracle) && (idExcluir == null || t.Id != idExcluir), cancellationToken);

    public void Agregar(Tarifario tarifario) => _context.Tarifarios.Add(tarifario);
    public void Actualizar(Tarifario tarifario) => _context.Tarifarios.Update(tarifario);
    public void Eliminar(Tarifario tarifario) => _context.Tarifarios.Remove(tarifario);
}

using Microsoft.EntityFrameworkCore;
using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Infrastructure.Persistence;
using UW.AdminPagoExpress.Shared.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    public async Task<List<Tarifario>> ListarAsync(CancellationToken ct)
    {
        return await _context.Tarifarios
            .AsNoTracking()
            .Include(t => t.Tratamiento)
            .ToListAsync(ct);
    }

    public async Task<bool> ExisteCodigoAsync(string codigoSpring, string codigoOracle, long? idExcluir, CancellationToken cancellationToken)
    {
        var query = _context.Tarifarios
            .AsNoTracking()
            .Where(t => t.CodigoSpring == codigoSpring || t.CodigoOracle == codigoOracle);

        if (idExcluir.HasValue)
        {
            var id = idExcluir.Value;
            query = query.Where(t => t.Id != id);
        }

        // 🔥 Usar CountAsync en lugar de AnyAsync
        var count = await query.CountAsync(cancellationToken);
        return count > 0;
    }

    public void Agregar(Tarifario tarifario) => _context.Tarifarios.Add(tarifario);
    public void Actualizar(Tarifario tarifario) => _context.Tarifarios.Update(tarifario);
    public void Eliminar(Tarifario tarifario) => _context.Tarifarios.Remove(tarifario);
}

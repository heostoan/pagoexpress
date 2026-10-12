using Microsoft.EntityFrameworkCore;
using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Infrastructure.Persistence;

namespace UW.AdminPagoExpress.Infrastructure.Repositories;

public class TratamientoRepository : ITratamientoRepository
{
    private readonly ApplicationDbContext _context;

    public TratamientoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Tratamiento?> ObtenerPorIdAsync(long id, CancellationToken cancellationToken)
        => await _context.Tratamientos.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    /*public async Task<List<Tratamiento>> ListarAsync(CancellationToken cancellationToken)
        => await _context.Tratamientos.AsNoTracking().ToListAsync(cancellationToken);*/

    public async Task<List<Tratamiento>> ListarAsync(CancellationToken ct)
    {
        return await _context.Tratamientos
            .AsNoTracking()
            .Include(t => t.Categoria)
            .ToListAsync(ct);
    }

    public async Task<bool> ExisteNombreAsync(string nombre, long? idExcluir, CancellationToken cancellationToken)
    {
        var query = _context.Tratamientos
            .AsNoTracking()
            .Where(t => t.Nombre == nombre);

        if (idExcluir.HasValue)
        {
            var id = idExcluir.Value;
            query = query.Where(t => t.Id != id);
        }

        // 🔥 Usar CountAsync en lugar de AnyAsync
        var count = await query.CountAsync(cancellationToken);
        return count > 0;
    }

    public void Agregar(Tratamiento tratamiento) => _context.Tratamientos.Add(tratamiento);
    public void Actualizar(Tratamiento tratamiento) => _context.Tratamientos.Update(tratamiento);
    public void Eliminar(Tratamiento tratamiento) => _context.Tratamientos.Remove(tratamiento);
}
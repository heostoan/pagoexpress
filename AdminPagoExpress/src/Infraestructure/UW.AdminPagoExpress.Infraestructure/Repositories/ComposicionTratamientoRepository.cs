using Microsoft.EntityFrameworkCore;
using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Domain.Entities;
using UW.AdminPagoExpress.Infrastructure.Persistence;

namespace UW.AdminPagoExpress.Infrastructure.Repositories;

public class ComposicionTratamientoRepository : IComposicionTratamientoRepository
{
    private readonly ApplicationDbContext _db;

    public ComposicionTratamientoRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<ComposicionTratamiento>> ListarPorTratamientoAsync(long idTratamientoPadre, CancellationToken ct = default)
    {
        return await _db.ComposicionTratamientos.Where(c => c.IdTratamiento == idTratamientoPadre).ToListAsync(ct);
    }

    public async Task<long> CrearAsync(ComposicionTratamiento entity, CancellationToken ct = default)
    {
        await _db.ComposicionTratamientos.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
        return entity.IdComposicionTratamiento;
    }

    public async Task CrearVariosAsync(IEnumerable<ComposicionTratamiento> entities, CancellationToken ct = default)
    {
        await _db.ComposicionTratamientos.AddRangeAsync(entities, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<List<ComposicionTratamiento>> ListarTodasAsync(CancellationToken ct = default)
    {
        return await _db.ComposicionTratamientos.AsNoTracking().ToListAsync(ct);
    }

    public async Task ActualizarEstadoAsync(long idComposicion, CancellationToken ct = default)
    {
        var c = await _db.ComposicionTratamientos.FirstOrDefaultAsync(x => x.IdComposicionTratamiento == idComposicion, ct);
        if (c == null) return;
        _db.ComposicionTratamientos.Update(c);
        await _db.SaveChangesAsync(ct);
    }
}

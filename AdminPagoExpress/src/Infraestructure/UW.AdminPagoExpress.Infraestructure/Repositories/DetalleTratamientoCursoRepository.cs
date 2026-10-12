using Microsoft.EntityFrameworkCore;
using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Domain.Entities;
using UW.AdminPagoExpress.Infrastructure.Persistence;

namespace UW.AdminPagoExpress.Infrastructure.Repositories;

public class DetalleTratamientoCursoRepository : IDetalleTratamientoCursoRepository
{
    private readonly ApplicationDbContext _db;

    public DetalleTratamientoCursoRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<DetalleTratamientoCurso>> ListarPorCursoAsync(long idCurso, CancellationToken ct = default)
    {
        return await _db.DetalleTratamientoCurso.Where(d => d.IdCurso == idCurso).ToListAsync(ct);
    }

    public async Task<List<DetalleTratamientoCurso>> ListarPorTratamientoAsync(long idTratamiento, CancellationToken ct = default)
    {
        return await _db.DetalleTratamientoCurso.Where(d => d.IdTratamiento == idTratamiento).ToListAsync(ct);
    }

    public async Task<long> CrearAsync(DetalleTratamientoCurso entity, CancellationToken ct = default)
    {
        await _db.DetalleTratamientoCurso.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
        return entity.IdDetalleTratamientoCurso;
    }

    public async Task CrearVariosAsync(IEnumerable<DetalleTratamientoCurso> entities, CancellationToken ct = default)
    {
        await _db.DetalleTratamientoCurso.AddRangeAsync(entities, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task ActualizarEstadoAsync(long idDetalle, int estado, CancellationToken ct = default)
    {
        var d = await _db.DetalleTratamientoCurso.FirstOrDefaultAsync(x => x.IdDetalleTratamientoCurso == idDetalle, ct);
        if (d == null) return;
        d.CambiarEstado(estado);
        _db.DetalleTratamientoCurso.Update(d);
        await _db.SaveChangesAsync(ct);
    }
}

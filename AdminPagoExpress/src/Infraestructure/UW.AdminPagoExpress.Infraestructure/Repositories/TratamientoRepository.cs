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

    public async Task<List<Tratamiento>> ListarAsync(CancellationToken cancellationToken)
        => await _context.Tratamientos.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<bool> ExisteNombreAsync(string nombre, long? idExcluir, CancellationToken cancellationToken)
        => await _context.Tratamientos
            .AsNoTracking()
            .AnyAsync(t => t.Nombre == nombre && (idExcluir == null || t.Id != idExcluir), cancellationToken);

    public void Agregar(Tratamiento tratamiento) => _context.Tratamientos.Add(tratamiento);
    public void Actualizar(Tratamiento tratamiento) => _context.Tratamientos.Update(tratamiento);
    public void Eliminar(Tratamiento tratamiento) => _context.Tratamientos.Remove(tratamiento);
}
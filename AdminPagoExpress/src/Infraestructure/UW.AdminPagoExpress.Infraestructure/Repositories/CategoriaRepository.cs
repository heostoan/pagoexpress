using Microsoft.EntityFrameworkCore;
using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Infrastructure.Persistence;

namespace UW.AdminPagoExpress.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly ApplicationDbContext _context;

    public CategoriaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria?> ObtenerPorIdAsync(long id, CancellationToken cancellationToken)
        => await _context.Set<Categoria>().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<List<Categoria>> ListarAsync(CancellationToken cancellationToken)
        => await _context.Set<Categoria>().AsNoTracking().ToListAsync(cancellationToken);

    public async Task<bool> ExisteNombreAsync(string nombre, long? idExcluir, CancellationToken cancellationToken)
        => await _context.Set<Categoria>()
            .AsNoTracking()
            .AnyAsync(c => c.Nombre == nombre && (idExcluir == null || c.Id != idExcluir), cancellationToken);

    public void Agregar(Categoria categoria) => _context.Set<Categoria>().Add(categoria);
    public void Actualizar(Categoria categoria) => _context.Set<Categoria>().Update(categoria);
    public void Eliminar(Categoria categoria) => _context.Set<Categoria>().Remove(categoria);
}

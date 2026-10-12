using Microsoft.EntityFrameworkCore;
using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Infrastructure.Persistence;

namespace UW.AdminPagoExpress.Infrastructure.Repositories;

public class CursoRepository : ICursoRepository
{
    private readonly ApplicationDbContext _context;

    public CursoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Curso?> ObtenerPorIdAsync(long id, CancellationToken cancellationToken)
        => await _context.Cursos.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<List<Curso>> ListarAsync(CancellationToken cancellationToken)
        => await _context.Cursos.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<bool> ExisteNombreAsync(string nombre, long? idExcluir, CancellationToken cancellationToken)
    {
        var query = _context.Cursos
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

    public void Agregar(Curso curso) => _context.Cursos.Add(curso);
    public void Actualizar(Curso curso) => _context.Cursos.Update(curso);
    public void Eliminar(Curso curso) => _context.Cursos.Remove(curso);
}

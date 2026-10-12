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
        => await _context.Cursos
            .AsNoTracking()
            .AnyAsync(c => c.Nombre == nombre && (idExcluir == null || c.Id != idExcluir), cancellationToken);

    public void Agregar(Curso curso) => _context.Cursos.Add(curso);
    public void Actualizar(Curso curso) => _context.Cursos.Update(curso);
    public void Eliminar(Curso curso) => _context.Cursos.Remove(curso);
}

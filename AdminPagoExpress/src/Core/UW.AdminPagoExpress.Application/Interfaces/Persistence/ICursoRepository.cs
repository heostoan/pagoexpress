using UW.AdminPagoExpress.Domain.Entities;

namespace UW.AdminPagoExpress.Application.Interfaces.Persistence;

public interface ICursoRepository
{
    Task<Curso?> ObtenerPorIdAsync(long id, CancellationToken cancellationToken);
    Task<List<Curso>> ListarAsync(CancellationToken cancellationToken);
    Task<bool> ExisteNombreAsync(string nombre, long? idExcluir, CancellationToken cancellationToken);
    void Agregar(Curso curso);
    void Actualizar(Curso curso);
    void Eliminar(Curso curso);
}

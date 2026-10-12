using UW.AdminPagoExpress.Domain.Entities;

namespace UW.AdminPagoExpress.Application.Interfaces.Persistence;

public interface ICategoriaRepository
{
    Task<Categoria?> ObtenerPorIdAsync(long id, CancellationToken cancellationToken);
    Task<List<Categoria>> ListarAsync(CancellationToken cancellationToken);
    Task<bool> ExisteNombreAsync(string nombre, long? idExcluir, CancellationToken cancellationToken);
    void Agregar(Categoria categoria);
    void Actualizar(Categoria categoria);
    void Eliminar(Categoria categoria);
}

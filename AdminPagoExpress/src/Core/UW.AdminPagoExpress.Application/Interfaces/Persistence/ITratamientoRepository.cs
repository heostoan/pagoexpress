using UW.AdminPagoExpress.Domain.Entities;

namespace UW.AdminPagoExpress.Application.Interfaces.Persistence;

public interface ITratamientoRepository
{
    Task<Tratamiento?> ObtenerPorIdAsync(long id, CancellationToken cancellationToken);
    Task<List<Tratamiento>> ListarAsync(CancellationToken cancellationToken);
    Task<bool> ExisteNombreAsync(string nombre, long? idExcluir, CancellationToken cancellationToken);
    void Agregar(Tratamiento tratamiento);
    void Actualizar(Tratamiento tratamiento);
    void Eliminar(Tratamiento tratamiento);
}
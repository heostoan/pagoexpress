using UW.AdminPagoExpress.Domain.Entities;

namespace UW.AdminPagoExpress.Application.Interfaces.Persistence;

public interface ITarifarioRepository
{
    Task<Tarifario?> ObtenerPorIdAsync(long id, CancellationToken cancellationToken);
    Task<List<Tarifario>> ListarAsync(CancellationToken cancellationToken);
    Task<bool> ExisteCodigoAsync(string codigoSpring, string codigoOracle, long? idExcluir, CancellationToken cancellationToken);
    void Agregar(Tarifario tarifario);
    void Actualizar(Tarifario tarifario);
    void Eliminar(Tarifario tarifario);
}

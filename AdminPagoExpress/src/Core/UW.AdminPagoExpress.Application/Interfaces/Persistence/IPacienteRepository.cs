using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Application.Interfaces.Persistence;

public interface IPacienteRepository
{
    Task<List<PacienteResponse>> ListarAsync(CancellationToken ct = default);
    Task<PacienteResponse?> ObtenerPorIdAsync(long id, CancellationToken ct = default);
    Task<long> CrearAsync(CrearPacienteRequest request, CancellationToken ct = default);
    Task<bool> ActualizarAsync(long id, ActualizarPacienteRequest request, CancellationToken ct = default);
    Task<bool> EliminarAsync(long id, CancellationToken ct = default);
}

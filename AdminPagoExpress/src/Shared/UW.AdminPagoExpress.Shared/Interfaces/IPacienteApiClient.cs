using UW.AdminPagoExpress.Shared.Common;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Shared.Interfaces;

public interface IPacienteApiClient
{
    Task<ApiResult<List<PacienteResponse>>> ListarAsync(CancellationToken ct = default);
    Task<ApiResult<PacienteResponse>> ObtenerPorIdAsync(long id, CancellationToken ct = default);
    Task<ApiResult<long>> CrearAsync(CrearPacienteRequest request, CancellationToken ct = default);
    Task<ApiResult> ActualizarAsync(long id, ActualizarPacienteRequest request, CancellationToken ct = default);
    Task<ApiResult> EliminarAsync(long id, CancellationToken ct = default);
}


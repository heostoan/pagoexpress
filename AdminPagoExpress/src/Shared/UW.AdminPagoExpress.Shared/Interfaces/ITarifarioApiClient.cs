using UW.AdminPagoExpress.Shared.Common;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Shared.Interfaces;

public interface ITarifarioApiClient
{
    Task<ApiResult<List<TarifarioResponse>>> ListarAsync(CancellationToken ct = default);
    Task<ApiResult<TarifarioResponse>> ObtenerPorIdAsync(long id, CancellationToken ct = default);
    Task<ApiResult<long>> CrearAsync(CrearTarifarioRequest request, CancellationToken ct = default);
    Task<ApiResult> ActualizarAsync(long id, ActualizarTarifarioRequest request, CancellationToken ct = default);
    Task<ApiResult> EliminarAsync(long id, CancellationToken ct = default);
}

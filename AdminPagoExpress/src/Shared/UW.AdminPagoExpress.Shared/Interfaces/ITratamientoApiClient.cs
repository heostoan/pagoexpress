using UW.AdminPagoExpress.Shared.Common;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Shared.Interfaces;

public interface ITratamientoApiClient
{
    Task<ApiResult<List<TratamientoResponse>>> ListarAsync(CancellationToken ct = default);
    Task<ApiResult<TratamientoResponse>> ObtenerPorIdAsync(long id, CancellationToken ct = default);
    Task<ApiResult<long>> CrearAsync(CrearTratamientoRequest request, CancellationToken ct = default);
    Task<ApiResult> ActualizarAsync(long id, ActualizarTratamientoRequest request, CancellationToken ct = default);
    Task<ApiResult> EliminarAsync(long id, CancellationToken ct = default);
}
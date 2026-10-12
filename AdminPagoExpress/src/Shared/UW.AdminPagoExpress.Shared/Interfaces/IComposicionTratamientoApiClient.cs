using UW.AdminPagoExpress.Shared.Common;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Shared.Interfaces;

public interface IComposicionTratamientoApiClient
{
    Task<ApiResult<List<TratamientoResponse>>> ListarTratamientosAsync(CancellationToken ct = default);
    Task<ApiResult<List<ComposicionTratamientoResponse>>> ListarPorTratamientoAsync(long idTratamiento, CancellationToken ct = default);
    Task<ApiResult<List<ComposicionTratamientoResponse>>> ListarTodasAsync(CancellationToken ct = default);
    Task<ApiResult> CrearVariosAsync(CrearComposicionTratamientoRequest request, CancellationToken ct = default);
    Task<ApiResult> ActualizarEstadoAsync(ActualizarEstadoComposicionTratamientoRequest request, CancellationToken ct = default);
    Task<ApiResult> ActualizarEstadosAsync(List<ActualizarEstadoComposicionTratamientoRequest> requests, CancellationToken ct = default);
}

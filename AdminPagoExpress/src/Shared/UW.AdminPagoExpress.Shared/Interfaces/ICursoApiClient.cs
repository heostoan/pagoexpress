using UW.AdminPagoExpress.Shared.Common;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Shared.Interfaces;

public interface ICursoApiClient
{
    Task<ApiResult<List<CursoResponse>>> ListarAsync(CancellationToken ct = default);
    Task<ApiResult<CursoResponse>> ObtenerPorIdAsync(long id, CancellationToken ct = default);
    Task<ApiResult<long>> CrearAsync(CrearCursoRequest request, CancellationToken ct = default);
    Task<ApiResult> ActualizarAsync(long id, ActualizarCursoRequest request, CancellationToken ct = default);
    Task<ApiResult> EliminarAsync(long id, CancellationToken ct = default);
}

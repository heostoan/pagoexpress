using UW.AdminPagoExpress.Shared.Common;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Shared.Interfaces;

public interface IDetalleTratamientoCursoApiClient
{
    Task<ApiResult<List<TratamientoResponse>>> ListarTratamientosAsync(CancellationToken ct = default);
    Task<ApiResult<List<DetalleTratamientoCursoResponse>>> ListarPorCursoAsync(long idCurso, CancellationToken ct = default);
    Task<ApiResult> CrearVariosAsync(CrearDetalleTratamientoCursoRequest request, CancellationToken ct = default);
    Task<ApiResult> ActualizarEstadoAsync(ActualizarEstadoDetalleTratamientoCursoRequest request, CancellationToken ct = default);
}

using UW.AdminPagoExpress.Shared.Common;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Shared.Interfaces;

public interface ICategoriaApiClient
{
    Task<ApiResult<List<CategoriaResponse>>> ListarAsync(CancellationToken ct = default);
    Task<ApiResult<CategoriaResponse>> ObtenerPorIdAsync(long id, CancellationToken ct = default);
    Task<ApiResult<long>> CrearAsync(CrearCategoriaRequest request, CancellationToken ct = default);
    Task<ApiResult> ActualizarAsync(long id, ActualizarCategoriaRequest request, CancellationToken ct = default);
    Task<ApiResult> EliminarAsync(long id, CancellationToken ct = default);
}

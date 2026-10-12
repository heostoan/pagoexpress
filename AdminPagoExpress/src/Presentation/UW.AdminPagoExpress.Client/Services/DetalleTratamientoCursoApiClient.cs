using System.Net.Http.Json;
using UW.AdminPagoExpress.Shared.Common;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Client.Services;

public class DetalleTratamientoCursoApiClient : IDetalleTratamientoCursoApiClient
{
    private readonly HttpClient _http;
    public DetalleTratamientoCursoApiClient(HttpClient http) => _http = http;

    public async Task<ApiResult<List<TratamientoResponse>>> ListarTratamientosAsync(CancellationToken ct = default)
    {
        try
        {
            var data = await _http.GetFromJsonAsync<List<TratamientoResponse>>("api/detalle-tratamiento-curso/tratamientos", ct);
            return ApiResult<List<TratamientoResponse>>.Success(data ?? new());
        }
        catch { return ApiResult<List<TratamientoResponse>>.Failure("Error al listar tratamientos."); }
    }

    public async Task<ApiResult<List<DetalleTratamientoCursoResponse>>> ListarPorCursoAsync(long idCurso, CancellationToken ct = default)
    {
        try
        {
            var data = await _http.GetFromJsonAsync<List<DetalleTratamientoCursoResponse>>($"api/detalle-tratamiento-curso/por-curso/{idCurso}", ct);
            return ApiResult<List<DetalleTratamientoCursoResponse>>.Success(data ?? new());
        }
        catch { return ApiResult<List<DetalleTratamientoCursoResponse>>.Failure("Error al listar detalles por curso."); }
    }

    public async Task<ApiResult> CrearVariosAsync(CrearDetalleTratamientoCursoRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/detalle-tratamiento-curso", request, ct);
            if (response.IsSuccessStatusCode) return ApiResult.Success();
            return ApiResult.Failure("Error al crear asociaciones.");
        }
        catch { return ApiResult.Failure("Error de conexión."); }
    }

    public async Task<ApiResult> ActualizarEstadoAsync(ActualizarEstadoDetalleTratamientoCursoRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PutAsJsonAsync("api/detalle-tratamiento-curso/estado", request, ct);
            if (response.IsSuccessStatusCode) return ApiResult.Success();
            return ApiResult.Failure("Error al actualizar estado.");
        }
        catch { return ApiResult.Failure("Error de conexión."); }
    }
}

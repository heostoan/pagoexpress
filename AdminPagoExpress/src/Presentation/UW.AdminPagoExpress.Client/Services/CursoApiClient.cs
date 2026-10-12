using System.Net;
using System.Net.Http.Json;
using UW.AdminPagoExpress.Shared.Common;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Client.Services;

public class CursoApiClient : ICursoApiClient
{
    private readonly HttpClient _http;

    public CursoApiClient(HttpClient http) => _http = http;

    public async Task<ApiResult<List<CursoResponse>>> ListarAsync(CancellationToken ct = default)
    {
        try
        {
            var data = await _http.GetFromJsonAsync<List<CursoResponse>>("api/cursos", ct);
            return ApiResult<List<CursoResponse>>.Success(data ?? new());
        }
        catch { return ApiResult<List<CursoResponse>>.Failure("Error al listar cursos."); }
    }

    public async Task<ApiResult<CursoResponse>> ObtenerPorIdAsync(long id, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.GetAsync($"api/cursos/{id}", ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return ApiResult<CursoResponse>.Failure("Curso no encontrado.");

            var data = await response.Content.ReadFromJsonAsync<CursoResponse>(cancellationToken: ct);
            return ApiResult<CursoResponse>.Success(data!);
        }
        catch { return ApiResult<CursoResponse>.Failure("Error de conexión."); }
    }

    public async Task<ApiResult<long>> CrearAsync(CrearCursoRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/cursos", request, ct);
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                var pd = await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken: ct);
                return ApiResult<long>.Failure(pd?.Detail ?? "Conflicto.");
            }
            if (!response.IsSuccessStatusCode)
                return ApiResult<long>.Failure("Error al crear.");

            var id = await response.Content.ReadFromJsonAsync<long>(cancellationToken: ct);
            return ApiResult<long>.Success(id);
        }
        catch { return ApiResult<long>.Failure("Error de conexión."); }
    }

    public async Task<ApiResult> ActualizarAsync(long id, ActualizarCursoRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"api/cursos/{id}", request, ct);
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                var pd = await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken: ct);
                return ApiResult.Failure(pd?.Detail ?? "Conflicto.");
            }
            return response.IsSuccessStatusCode ? ApiResult.Success() : ApiResult.Failure("Error al actualizar.");
        }
        catch { return ApiResult.Failure("Error de conexión."); }
    }

    public async Task<ApiResult> EliminarAsync(long id, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/cursos/{id}", ct);
            return response.IsSuccessStatusCode ? ApiResult.Success() : ApiResult.Failure("Error al eliminar.");
        }
        catch { return ApiResult.Failure("Error de conexión."); }
    }
}

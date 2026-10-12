using System.Net;
using System.Net.Http.Json;
using UW.AdminPagoExpress.Shared.Common;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Client.Services;

public class CategoriaApiClient : ICategoriaApiClient
{
    private readonly HttpClient _http;

    public CategoriaApiClient(HttpClient http) => _http = http;

    public async Task<ApiResult<List<CategoriaResponse>>> ListarAsync(CancellationToken ct = default)
    {
        try
        {
            var data = await _http.GetFromJsonAsync<List<CategoriaResponse>>("api/categorias", ct);
            return ApiResult<List<CategoriaResponse>>.Success(data ?? new());
        }
        catch { return ApiResult<List<CategoriaResponse>>.Failure("Error al listar categorías."); }
    }

    public async Task<ApiResult<CategoriaResponse>> ObtenerPorIdAsync(long id, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.GetAsync($"api/categorias/{id}", ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return ApiResult<CategoriaResponse>.Failure("Categoría no encontrada.");

            var data = await response.Content.ReadFromJsonAsync<CategoriaResponse>(cancellationToken: ct);
            return ApiResult<CategoriaResponse>.Success(data!);
        }
        catch { return ApiResult<CategoriaResponse>.Failure("Error de conexión."); }
    }

    public async Task<ApiResult<long>> CrearAsync(CrearCategoriaRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/categorias", request, ct);
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

    public async Task<ApiResult> ActualizarAsync(long id, ActualizarCategoriaRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"api/categorias/{id}", request, ct);
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
            var response = await _http.DeleteAsync($"api/categorias/{id}", ct);
            return response.IsSuccessStatusCode ? ApiResult.Success() : ApiResult.Failure("Error al eliminar.");
        }
        catch { return ApiResult.Failure("Error de conexión."); }
    }
}

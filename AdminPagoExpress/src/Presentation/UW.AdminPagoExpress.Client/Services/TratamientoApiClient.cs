using System.Net;
using System.Net.Http.Json;
using UW.AdminPagoExpress.Shared.Common;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Client.Services;

public class TratamientoApiClient : ITratamientoApiClient
{
    private readonly HttpClient _http;

    public TratamientoApiClient(HttpClient http)
        => _http = http;

    public async Task<ApiResult<List<TratamientoResponse>>> ListarAsync(CancellationToken ct = default)
    {
        try
        {
            var data = await _http.GetFromJsonAsync<List<TratamientoResponse>>("api/tratamientos", ct);
            return ApiResult<List<TratamientoResponse>>.Success(data ?? new());
        }
        catch { return ApiResult<List<TratamientoResponse>>.Failure("Error al listar tratamientos."); }
    }

    public async Task<ApiResult<TratamientoResponse>> ObtenerPorIdAsync(long id, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.GetAsync($"api/tratamientos/{id}", ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return ApiResult<TratamientoResponse>.Failure("Tratamiento no encontrado.");

            var data = await response.Content.ReadFromJsonAsync<TratamientoResponse>(cancellationToken: ct);
            return ApiResult<TratamientoResponse>.Success(data!);
        }
        catch { return ApiResult<TratamientoResponse>.Failure("Error de conexión."); }
    }

    public async Task<ApiResult<long>> CrearAsync(CrearTratamientoRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/tratamientos", request, ct);
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

    public async Task<ApiResult> ActualizarAsync(long id, ActualizarTratamientoRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"api/tratamientos/{id}", request, ct);
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
            var response = await _http.DeleteAsync($"api/tratamientos/{id}", ct);
            return response.IsSuccessStatusCode ? ApiResult.Success() : ApiResult.Failure("Error al eliminar.");
        }
        catch { return ApiResult.Failure("Error de conexión."); }
    }
}
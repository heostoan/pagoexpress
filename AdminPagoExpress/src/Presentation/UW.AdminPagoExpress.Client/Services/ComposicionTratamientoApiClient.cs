using System.Net.Http.Json;
using UW.AdminPagoExpress.Shared.Common;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Client.Services;

public class ComposicionTratamientoApiClient : IComposicionTratamientoApiClient
{
    private readonly HttpClient _http;
    public ComposicionTratamientoApiClient(HttpClient http) => _http = http;

    public async Task<ApiResult<List<TratamientoResponse>>> ListarTratamientosAsync(CancellationToken ct = default)
    {
        try
        {
            var data = await _http.GetFromJsonAsync<List<TratamientoResponse>>("api/composicion-tratamiento/tratamientos", ct);
            return ApiResult<List<TratamientoResponse>>.Success(data ?? new());
        }
        catch { return ApiResult<List<TratamientoResponse>>.Failure("Error al listar tratamientos."); }
    }

    public async Task<ApiResult<List<ComposicionTratamientoResponse>>> ListarPorTratamientoAsync(long idTratamiento, CancellationToken ct = default)
    {
        try
        {
            var data = await _http.GetFromJsonAsync<List<ComposicionTratamientoResponse>>($"api/composicion-tratamiento/por-tratamiento/{idTratamiento}", ct);
            return ApiResult<List<ComposicionTratamientoResponse>>.Success(data ?? new());
        }
        catch { return ApiResult<List<ComposicionTratamientoResponse>>.Failure("Error al listar composiciones."); }
    }

    public async Task<ApiResult<List<ComposicionTratamientoResponse>>> ListarTodasAsync(CancellationToken ct = default)
    {
        try
        {
            var data = await _http.GetFromJsonAsync<List<ComposicionTratamientoResponse>>("api/composicion-tratamiento/todas", ct);
            return ApiResult<List<ComposicionTratamientoResponse>>.Success(data ?? new());
        }
        catch { return ApiResult<List<ComposicionTratamientoResponse>>.Failure("Error al listar composiciones."); }
    }

    public async Task<ApiResult> CrearVariosAsync(CrearComposicionTratamientoRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/composicion-tratamiento", request, ct);
            if (response.IsSuccessStatusCode) return ApiResult.Success();
            return ApiResult.Failure("Error al crear composiciones.");
        }
        catch { return ApiResult.Failure("Error de conexión."); }
    }

    public async Task<ApiResult> ActualizarEstadoAsync(ActualizarEstadoComposicionTratamientoRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PutAsJsonAsync("api/composicion-tratamiento/estado", request, ct);
            if (response.IsSuccessStatusCode) return ApiResult.Success();
            return ApiResult.Failure("Error al actualizar estado.");
        }
        catch { return ApiResult.Failure("Error de conexión."); }
    }

    public async Task<ApiResult> ActualizarEstadosAsync(List<ActualizarEstadoComposicionTratamientoRequest> requests, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PutAsJsonAsync("api/composicion-tratamiento/estados", requests, ct);
            if (response.IsSuccessStatusCode) return ApiResult.Success();
            return ApiResult.Failure("Error al actualizar estados.");
        }
        catch { return ApiResult.Failure("Error de conexión."); }
    }
}

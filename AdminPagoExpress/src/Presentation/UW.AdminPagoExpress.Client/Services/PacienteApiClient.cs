using System.Net;
using System.Net.Http.Json;
using UW.AdminPagoExpress.Shared.Common;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Client.Services;

public class PacienteApiClient : IPacienteApiClient
{
    private readonly HttpClient _http;

    public PacienteApiClient(HttpClient http) => _http = http;

    public async Task<ApiResult<List<PacienteResponse>>> ListarAsync(CancellationToken ct = default)
    {
        try
        {
            var data = await _http.GetFromJsonAsync<List<PacienteResponse>>("api/pacientes", ct);
            return ApiResult<List<PacienteResponse>>.Success(data ?? new());
        }
        catch { return ApiResult<List<PacienteResponse>>.Failure("Error al listar pacientes."); }
    }

    public async Task<ApiResult<PacienteResponse>> ObtenerPorIdAsync(long id, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.GetAsync($"api/pacientes/{id}", ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return ApiResult<PacienteResponse>.Failure("Paciente no encontrado.");

            var data = await response.Content.ReadFromJsonAsync<PacienteResponse>(cancellationToken: ct);
            return ApiResult<PacienteResponse>.Success(data!);
        }
        catch { return ApiResult<PacienteResponse>.Failure("Error de conexión."); }
    }

    public async Task<ApiResult<long>> CrearAsync(CrearPacienteRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/pacientes", request, ct);
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

    public async Task<ApiResult> ActualizarAsync(long id, ActualizarPacienteRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"api/pacientes/{id}", request, ct);
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
            var response = await _http.DeleteAsync($"api/pacientes/{id}", ct);
            return response.IsSuccessStatusCode ? ApiResult.Success() : ApiResult.Failure("Error al eliminar.");
        }
        catch { return ApiResult.Failure("Error de conexión."); }
    }
}

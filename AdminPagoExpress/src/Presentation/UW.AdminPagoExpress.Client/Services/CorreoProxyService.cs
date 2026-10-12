using System.Net.Http.Json;
using UW.AdminPagoExpress.Shared.Endpoints;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Request;

namespace UW.AdminPagoExpress.Client.Services;

internal class CorreoProxyService : ICorreoService
{
    private readonly HttpClient httpClient;

    public CorreoProxyService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<bool> EnviarCorreoUsuarioAsync(EnviarCorreoUsuarioRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(CorreoEndpoints.EnviarCorreoUsuario, request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
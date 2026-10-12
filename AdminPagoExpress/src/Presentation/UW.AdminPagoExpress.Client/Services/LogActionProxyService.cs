using System.Net.Http.Json;
using UW.AdminPagoExpress.Shared.Endpoints;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Request;

namespace UW.AdminPagoExpress.Client.Services;

public class LogActionProxyService : ILogActionService
{
    private readonly HttpClient httpClient;

    public LogActionProxyService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<bool> GuardarLogActionAsync(CreateLogRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(LogActionsEndpoint.GuardarActionLog, request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return false;
    }
}
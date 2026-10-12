
using System.Net.Http.Json;
using UW.AdminPagoExpress.Shared.Dtos;
using UW.AdminPagoExpress.Shared.Endpoints;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Request;
namespace UW.AdminPagoExpress.Client.Services;

public class OCIProxyService : IOCIService
{
    private readonly HttpClient httpClient;

    public OCIProxyService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<OCIDto>> GetListOfConsult(OCIRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(OCIEndpoint.PostOCIEndpoint, request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<OCIDto>>()??new();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}

using System.Net.Http.Json;
using UW.AdminPagoExpress.Shared.Endpoints;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Request.LoginMatriculaIntegral;

namespace UW.AdminPagoExpress.Client.Services.Login;

internal class UrlCryptoProxyService : IUrlCryptoService
{
    private readonly HttpClient httpClient;

    public UrlCryptoProxyService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<string> GetDesEncryptTextAspAsync(GetDesEncryptTextAspRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(UrlCryptoEndpoints.GetDesEncryptTextAsp, request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<string>();
    }

    public async Task<string> GetEncryptUrlAsync(GetEncryptUrlRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(UrlCryptoEndpoints.GetEncryptUrl, request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<string>();
    }
}
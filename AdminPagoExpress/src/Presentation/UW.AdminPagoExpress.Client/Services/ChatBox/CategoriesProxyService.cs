using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using UW.AdminPagoExpress.Shared.Dtos;
using UW.AdminPagoExpress.Shared.Dtos.ChatBox;
using UW.AdminPagoExpress.Shared.Endpoints;
using UW.AdminPagoExpress.Shared.Interfaces.ChatBox;
using UW.AdminPagoExpress.Shared.Request;

namespace UW.AdminPagoExpress.Client.Services.ChatBox;

public class CategoriesProxyService : ICategoriesService
{
    private readonly HttpClient httpClient;
    private readonly NavigationManager navigationManager;

    public CategoriesProxyService(HttpClient httpClient, NavigationManager navigationManager)
    {
        this.httpClient = httpClient;
        this.navigationManager = navigationManager;
    }

    public async Task<IEnumerable<CategoriesDTO>> GetCategoriesAsync()
    {
        try
        {
            var response = await httpClient.GetAsync(ChatboxEndpoints.GetCategoriesEndpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<CategoriesDTO>>() ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<CategoriesDTO> GetCategoryAsync(int Id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{ChatboxEndpoints.GetCategoryEndpoint}{Id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CategoriesDTO>() ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<MensajeResponseDto> UpdateCategoryAsync(UpdateCategoryRequest request)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync(ChatboxEndpoints.DeleteCategoryEndpoint, request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MensajeResponseDto>() ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<MensajeResponseDto> DeleteCategoryAsync(int Id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"{ChatboxEndpoints.DeleteCategoryEndpoint}{Id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MensajeResponseDto>() ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}

using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using UW.AdminPagoExpress.Shared.Dtos.User;
using UW.AdminPagoExpress.Shared.Endpoints;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Request.Login;
using UW.AdminPagoExpress.Shared.Request.LoginMicrosoft;

namespace UW.AdminPagoExpress.Client.Services.Login;

internal class UserProxyService : IUserService
{
    private readonly HttpClient httpClient;
    private readonly NavigationManager navigationManager;

    public UserProxyService(HttpClient httpClient, NavigationManager navigationManager)
    {
        this.httpClient = httpClient;
        this.navigationManager = navigationManager;
    }

    public async Task<bool> LogOutAsync()
    {
        var response = await httpClient.PostAsync(UserEndpoints.UserLogout, null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<bool> RemoveAuthTokenAsync()
    {
        var response = await httpClient.PostAsync(UserEndpoints.RemoveAuthToken, null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<ValidateAccessDto> ValidateAccessAsync(GetValidateAccessRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(UserEndpoints.UserValidateAccess, request);
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadAsStringAsync();
        return await response.Content.ReadFromJsonAsync<ValidateAccessDto>();
    }

    public async Task<ValidateAccessDto> ValidateAccessWihoutPasswordAsync(
        GetValidateAccessWithoutPasswordRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(UserEndpoints.UserValidateAccessWithoutPassword, request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ValidateAccessDto>();
    }

    public async Task<UserClaimDto> GetUserClaimsAsync()
    {
        try
        {
            var response = await httpClient.PostAsync(UserEndpoints.UserGetClaims, null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserClaimDto>();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == HttpStatusCode.Unauthorized) navigationManager.NavigateTo("/auth/logout");
            throw;
        }
    }

    public async Task<UserGraphDto> GetUserGraphAsync()
    {
        var response = await httpClient.PostAsync(UserEndpoints.UserGetGraph, null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserGraphDto>();
    }

    public async Task<string> GetUserByEmailAsync(GetUserByEmailRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(UserEndpoints.UserGetByEmail, request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<string>();
    }

    public async Task<string> GetUserPhotoAsync()
    {
        var response = await httpClient.PostAsync(UserEndpoints.UserGetPhoto, null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<string>();
    }

    public async Task<string> GetLocalIPAddressAsync()
    {
        var response = await httpClient.PostAsync(UserEndpoints.Ip, null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<string>();
    }
}
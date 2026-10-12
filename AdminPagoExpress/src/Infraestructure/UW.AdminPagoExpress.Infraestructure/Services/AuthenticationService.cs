using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using UW.AdminPagoExpress.Shared.Dtos.User;
using Interfaces_IAuthenticationService = UW.AdminPagoExpress.Application.Interfaces.IAuthenticationService;

namespace UW.AdminPagoExpress.Infraestructure.Services;

public class AuthenticationService : Interfaces_IAuthenticationService
{
    private readonly GraphServiceClient _graphClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration config;

    public AuthenticationService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration,
        GraphServiceClient graphClient)
    {
        config = configuration;
        _httpContextAccessor = httpContextAccessor;
        _graphClient = graphClient;
    }

    public async Task<UserGraphDto> GetAuthenticatedUserAsync()
    {
        var userGraph = await _graphClient.Me
            .Request()
            .WithAuthenticationScheme(OpenIdConnectDefaults.AuthenticationScheme)
            .GetAsync();

        var userGraphDto = new UserGraphDto
        {
            JobTitle = userGraph.JobTitle,
            Mail = userGraph.Mail
        };

        return userGraphDto;
    }

    public void SetAuthCookie(string nombres)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) throw new InvalidOperationException("HttpContext is not available.");

        var token = GenerateToken(nombres);

        httpContext.Response.Cookies.Append("authToken", token, new CookieOptions
        {
            HttpOnly = true, // Impide que el token sea accesible vía JavaScript
            Secure = true, // Solo se enviará sobre conexiones HTTPS
            SameSite = SameSiteMode
                .Strict, //las cookies solo se enviarán con solicitudes que provienen del mismo sitio que emitió la cookie
            Expires = DateTime.UtcNow.AddMinutes(120)
        });
    }


    public string GenerateToken(string nombres)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, nombres),
            new Claim(ClaimTypes.Role, "User")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(120),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateTokenFromCookie()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) throw new InvalidOperationException("HttpContext is not available.");

        // Intentar obtener la cookie con el token
        if (!httpContext.Request.Cookies.TryGetValue("authToken",
                out var token)) return false; // No se encontró la cookie

        // Validar el token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out _);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task SignInAsync(ClaimsPrincipal principal, bool isPersistent, DateTimeOffset? expiresUtc = null)
    {
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = isPersistent,
            ExpiresUtc = expiresUtc
        };

        if (_httpContextAccessor.HttpContext == null)
            throw new InvalidOperationException("HttpContext is not available.");

        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            authProperties);
    }

    public async Task SignOutAsync()
    {
        if (_httpContextAccessor.HttpContext == null)
            throw new InvalidOperationException("HttpContext is not available.");
        RemoveAuthCookie();
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public void RemoveAuthCookie()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) throw new InvalidOperationException("HttpContext is not available.");
        if (httpContext.Request.Cookies.ContainsKey("authToken"))
            httpContext.Response.Cookies.Append("authToken", string.Empty, new CookieOptions
            {
                HttpOnly = true, // Para mantener las mejores prácticas
                Secure = true, // Solo sobre HTTPS
                SameSite = SameSiteMode.Strict, // Asegurar que la cookie solo se envíe en el mismo sitio
                Expires = DateTime.UtcNow.AddDays(-1) // Establecer la expiración a una fecha pasada para eliminarla
            });
    }
}
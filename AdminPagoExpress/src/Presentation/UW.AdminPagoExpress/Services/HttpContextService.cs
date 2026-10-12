using UW.AdminPagoExpress.Shared.Interfaces;

namespace UW.AdminPagoExpress.Services;
public class HttpContextService : IHttpContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string ObtenerIpReal()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null) return "Desconocido";

        var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        string ip;

        if (!string.IsNullOrEmpty(forwardedFor))
        {
            ip = forwardedFor.Split(',').First().Trim();
        }
        else
        {
            ip = context.Request.Headers["X-Real-IP"].FirstOrDefault()
                 ?? context.Connection.RemoteIpAddress?.ToString();
        }

        return ip == "::1" ? "127.0.0.1" : ip ?? "Desconocido";
    }
}

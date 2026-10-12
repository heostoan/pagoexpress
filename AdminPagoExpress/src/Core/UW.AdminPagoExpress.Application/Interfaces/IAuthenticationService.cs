using System.Security.Claims;
using UW.AdminPagoExpress.Shared.Dtos.User;

namespace UW.AdminPagoExpress.Application.Interfaces;

public interface IAuthenticationService
{
    Task SignInAsync(ClaimsPrincipal principal, bool isPersistent, DateTimeOffset? expiresUtc = null);
    Task SignOutAsync();
    void SetAuthCookie(string nombres);
    string GenerateToken(string nombres);
    bool ValidateTokenFromCookie();
    Task<UserGraphDto> GetAuthenticatedUserAsync();
    void RemoveAuthCookie();
}
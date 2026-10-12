using UW.AdminPagoExpress.Shared.Dtos.User;
using UW.AdminPagoExpress.Shared.Request.Login;
using UW.AdminPagoExpress.Shared.Request.LoginMicrosoft;

namespace UW.AdminPagoExpress.Shared.Interfaces;

public interface IUserService
{
    Task<bool> LogOutAsync();
    Task<bool> RemoveAuthTokenAsync();
    Task<ValidateAccessDto> ValidateAccessAsync(GetValidateAccessRequest request);
    Task<ValidateAccessDto> ValidateAccessWihoutPasswordAsync(GetValidateAccessWithoutPasswordRequest request);
    Task<UserClaimDto> GetUserClaimsAsync();
    Task<UserGraphDto> GetUserGraphAsync();
    Task<string> GetUserByEmailAsync(GetUserByEmailRequest request);
    Task<string> GetUserPhotoAsync();
    Task<string> GetLocalIPAddressAsync();
}
using UW.AdminPagoExpress.Shared.Request.LoginMatriculaIntegral;

namespace UW.AdminPagoExpress.Shared.Interfaces;

public interface IUrlCryptoService
{
    Task<string> GetDesEncryptTextAspAsync(GetDesEncryptTextAspRequest request);
    Task<string> GetEncryptUrlAsync(GetEncryptUrlRequest request);
}
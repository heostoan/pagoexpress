using UW.AdminPagoExpress.Application.Features.UrlCryp.Queries.GetDesEncryptTextAsp;
using UW.AdminPagoExpress.Application.Features.UrlCryp.Queries.GetEncryptUrl;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Request.LoginMatriculaIntegral;

namespace UW.AdminPagoExpress.Services;

public class UrlCryptoService : IUrlCryptoService
{
    private readonly ISender sender;

    public UrlCryptoService(ISender sender)
    {
        this.sender = sender;
    }

    public async Task<string> GetDesEncryptTextAspAsync(GetDesEncryptTextAspRequest request)
    {
        var result = new GetDesEncryptTextAspQuery(request.encryptedBase64);
        return await sender.Send(result);
    }

    public async Task<string> GetEncryptUrlAsync(GetEncryptUrlRequest request)
    {
        var result = new GetEncryptUrlQuery(request.text);
        return await sender.Send(result);
    }
}
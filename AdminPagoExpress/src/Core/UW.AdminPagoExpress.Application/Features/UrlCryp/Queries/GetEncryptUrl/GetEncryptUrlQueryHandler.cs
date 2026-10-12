using UW.AdminPagoExpress.Application.Base.Abstractions.Messaging;
using UW.AdminPagoExpress.Domain.Helpers;
using UW.AdminPagoExpress.Shared.Request.LoginMatriculaIntegral;

namespace UW.AdminPagoExpress.Application.Features.UrlCryp.Queries.GetEncryptUrl;

public record GetEncryptUrlQuery(
    string text
) : GetEncryptUrlRequest(text), ICommand<string>;

public class GetEncryptUrlQueryHandler : ICommandHandler<GetEncryptUrlQuery, string>
{
    private readonly UrlCrypto _crypto;

    public GetEncryptUrlQueryHandler(UrlCrypto crypto)
    {
        _crypto = crypto;
    }

    public async Task<string> Handle(GetEncryptUrlQuery query, CancellationToken cancellationToken)
    {
        var response = _crypto.EncryptUrl(query.text);

        return response;
    }
}
using UW.AdminPagoExpress.Application.Base.Abstractions.Messaging;
using UW.AdminPagoExpress.Domain.Helpers;
using UW.AdminPagoExpress.Shared.Request.LoginMatriculaIntegral;

namespace UW.AdminPagoExpress.Application.Features.UrlCryp.Queries.GetDesEncryptTextAsp;

public record GetDesEncryptTextAspQuery(
    string text
) : GetDesEncryptTextAspRequest(text), ICommand<string>;

public class GetDesEncryptTextAspQueryHandler : ICommandHandler<GetDesEncryptTextAspQuery, string>
{
    private readonly UrlCrypto _crypto;

    public GetDesEncryptTextAspQueryHandler(UrlCrypto crypto)
    {
        _crypto = crypto;
    }

    public async Task<string> Handle(GetDesEncryptTextAspQuery query, CancellationToken cancellationToken)
    {
        var response = _crypto.DecryptTextAspClasic(query.text);

        return response;
    }
}
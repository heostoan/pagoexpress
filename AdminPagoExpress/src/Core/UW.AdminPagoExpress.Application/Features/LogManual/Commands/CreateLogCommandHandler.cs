using UW.AdminPagoExpress.Application.Base.Abstractions.Messaging;

namespace UW.AdminPagoExpress.Application.Features.LogManual.Commands;

public record CreateLogCommand(
    string Mensaje
) : ICommand<ErrorOr<bool>>;

public class CreateLogCommandHandler : ICommandHandler<CreateLogCommand, ErrorOr<bool>>
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CreateLogCommandHandler(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public Task<ErrorOr<bool>> Handle(CreateLogCommand request, CancellationToken cancellationToken)
    {
        var idSesion = httpContextAccessor.HttpContext?.User?.FindFirst("IdSesion")?.Value;
        var user = httpContextAccessor.HttpContext?.User?.FindFirst("User")?.Value;

        Log.ForContext("IdSesion", idSesion)
            .ForContext("Usuario", user)
            .Information(request.Mensaje);

        return Task.FromResult<ErrorOr<bool>>(true);
    }
}
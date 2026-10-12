using UW.AdminPagoExpress.Application.Features.LogManual.Commands;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Request;

namespace UW.AdminPagoExpress.Services;

public class LogActionService : ILogActionService
{
    private readonly ISender sender;

    public LogActionService(ISender sender)
    {
        this.sender = sender;
    }

    public async Task<bool> GuardarLogActionAsync(CreateLogRequest request)
    {
        var result = new CreateLogCommand(request.Mensaje);
        var resultCommand = await sender.Send(result);
        return resultCommand.Value;
    }
}
using UW.AdminPagoExpress.Shared.Request;

namespace UW.AdminPagoExpress.Shared.Interfaces;

public interface ILogActionService
{
    Task<bool> GuardarLogActionAsync(CreateLogRequest request);

}
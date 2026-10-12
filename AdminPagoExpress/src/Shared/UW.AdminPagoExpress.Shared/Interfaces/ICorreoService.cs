using UW.AdminPagoExpress.Shared.Request;

namespace UW.AdminPagoExpress.Shared.Interfaces;

public interface ICorreoService
{
    Task<bool> EnviarCorreoUsuarioAsync(EnviarCorreoUsuarioRequest request);
}
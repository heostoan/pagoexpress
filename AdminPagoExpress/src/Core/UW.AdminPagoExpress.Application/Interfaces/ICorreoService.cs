namespace UW.AdminPagoExpress.Application.Interfaces;

public interface ICorreoService
{
    Task<bool> EnviarCorreoUsuarioAsync(string destinatario, string asunto, string cuerpo,
        List<string>? bccDestinatarios = null);

    Task<bool> EnviarCorreoUsuarioApiSMTPAsync(string proyecto, string destinatario, string asunto, string cuerpo,
        List<string>? bccDestinatarios = null);

    Task<bool> EnviarCorreoUsuarioApiGraphAsync(string proyecto, string destinatario, string asunto, string cuerpo,
        List<string>? bccDestinatarios = null);

    bool GetEnviarCorreo();

    bool GetTestCorreo();

    string GetCorreosDePrueba();
}
namespace UW.AdminPagoExpress.Shared.Request.Login;

public record GetValidateAccessRequest(
    string Institucion,
    string Tipo_Usuario,
    string Usuario,
    string Password,
    string idSesion);
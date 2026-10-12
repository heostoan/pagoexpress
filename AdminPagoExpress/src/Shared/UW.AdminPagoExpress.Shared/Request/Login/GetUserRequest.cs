namespace UW.AdminPagoExpress.Shared.Request.Login;

public record GetUserRequest(
    string Institucion,
    string Tipo_Usuario,
    string Usuario,
    string Password,
    string idSesion
);
namespace UW.AdminPagoExpress.Shared.Request.LoginMicrosoft;

public record GetUserWithoutPasswordRequest(
    string Institucion,
    string Tipo_Usuario,
    string Usuario,
    string idSesion
);
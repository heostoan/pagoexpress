namespace UW.AdminPagoExpress.Shared.Request.LoginMicrosoft;

public record GetValidateAccessWithoutPasswordRequest(
    string Institucion,
    string Tipo_Usuario,
    string Usuario,
    string idSesion
);
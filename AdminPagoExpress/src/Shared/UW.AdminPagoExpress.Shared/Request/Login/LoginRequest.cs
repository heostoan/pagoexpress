namespace UW.AdminPagoExpress.Shared.Request.Login;

public record LoginRequest(
    string Nombres,
    string TipoUsuario,
    string Usuario,
    string Codigo,
    string IdSesion,
    string Carrera,
    string Institucion,
    string CarreraDescripcion,
    bool RememberMe,
    int pkCodigo,
    string Grado,
    List<string> Correos,
    string IPUsuario,
    bool MatriculaIntegral = false,
    string MatriculaIntegralCodigoUsuario = default!
);
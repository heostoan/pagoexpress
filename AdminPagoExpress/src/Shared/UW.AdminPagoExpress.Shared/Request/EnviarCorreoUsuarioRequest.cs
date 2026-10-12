namespace UW.AdminPagoExpress.Shared.Request;

public record EnviarCorreoUsuarioRequest(
    string Opcion,
    string Institucion,
    string CodigoAlumno,
    string Carrera,
    string Periodo,
    string Mes,
    string NombreAlumno,
    string CorreoToSend
);
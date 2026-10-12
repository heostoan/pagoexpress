namespace UW.AdminPagoExpress.Shared.Response;

public record PacienteResponse(
    long Id,
    string TipoDocumento,
    string DocIdentidad,
    string Nombres,
    string ApellidoPaterno,
    string ApellidoMaterno,
    string Genero,
    string Celular,
    string Correo,
    DateTime? FechaNacimiento,
    DateTime? FechaCreacion,
    DateTime? FechaModificacion,
    long IdAlumno,
    long IdFirma,
    string Estado
);

namespace UW.AdminPagoExpress.Shared.Request;

public class ActualizarPacienteRequest
{
    public string TipoDocumento { get; set; } = string.Empty;
    public string DocIdentidad { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string ApellidoMaterno { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public string Celular { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
    public string Estado { get; set; } = "1";
    public long IdAlumno { get; set; }
    public long IdFirma { get; set; }
}

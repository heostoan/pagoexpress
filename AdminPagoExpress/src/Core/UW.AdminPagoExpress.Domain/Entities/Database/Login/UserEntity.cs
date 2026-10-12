namespace UW.AdminPagoExpress.Domain.Entities.Database.Login;

public class UserEntity
{
    public string? ESTADO { get; set; }
    public string? MENSAJE { get; set; }
    public string? ALUMNO { get; set; }
    public string? NOMBRES { get; set; }
    public string? INSTITUCION { get; set; }
    public string? INSTITUCION_DES { get; set; }
    public string? GRADO { get; set; }
    public string? GRADO_DES { get; set; }
    public string? CARRERA { get; set; }
    public string? CARRERA_DES { get; set; }
    public string? FACULTAD { get; set; }

    public string? EMAILS { get; set; }

    public int PKCODIGO { get; set; }
    public byte[]? Foto { get; set; }
}
namespace UW.AdminPagoExpress.Shared.Dtos.User;

public class UserDto
{
    public string Estado { get; set; } = default!;
    public string Mensaje { get; set; } = default!;
    public string Alumno { get; set; } = default!;
    public string Nombres { get; set; } = default!;
    public string Institucion { get; set; } = default!;
    public string InstitucionDes { get; set; } = default!;
    public string Grado { get; set; } = default!;
    public string GradoDes { get; set; } = default!;
    public string Carrera { get; set; } = default!;
    public string CarreraDescripcion { get; set; } = default!;
    public string Facultad { get; set; } = default!;
    public byte[] Foto { get; set; } = default!;
    public string Token { get; set; } = default!;
    public List<string> Correos { get; set; } = default!;
    public int PKCODIGO { get; set; }
}
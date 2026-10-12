namespace UW.AdminPagoExpress.Shared.Dtos.User;

public class UsuarioNavBarParametroDto
{
    public string Nombres { get; set; } = default!;
    public string CarreraDescripcion { get; set; } = default!;
    public string CodigoAlumno { get; set; } = default!;

    public bool Matriculado { get; set; } = default!;
    public string Ip { get; set; } = default!;
}
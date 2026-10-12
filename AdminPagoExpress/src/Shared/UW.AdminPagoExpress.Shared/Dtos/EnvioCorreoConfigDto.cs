namespace UW.AdminPagoExpress.Shared.Dtos;

public class EnvioCorreoConfigDto
{
    public bool Activo { get; set; }
    public bool Test { get; set; }
    public List<string> CorreosTest { get; set; } = new();
}
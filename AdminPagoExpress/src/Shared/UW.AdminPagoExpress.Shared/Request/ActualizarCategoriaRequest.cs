namespace UW.AdminPagoExpress.Shared.Request;

public class ActualizarCategoriaRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Estado { get; set; } = "1";
    public string? Descripcion { get; set; }

    public ActualizarCategoriaRequest() { }

    public ActualizarCategoriaRequest(string nombre, string estado, string? descripcion)
    {
        Nombre = nombre;
        Estado = estado;
        Descripcion = descripcion;
    }
}

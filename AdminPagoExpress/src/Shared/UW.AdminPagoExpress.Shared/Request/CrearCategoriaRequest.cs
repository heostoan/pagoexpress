namespace UW.AdminPagoExpress.Shared.Request;

public class CrearCategoriaRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Estado { get; set; } = "1";
    public string? Descripcion { get; set; }

    public CrearCategoriaRequest() { }

    public CrearCategoriaRequest(string nombre, string estado, string? descripcion)
    {
        Nombre = nombre;
        Estado = estado;
        Descripcion = descripcion;
    }
}

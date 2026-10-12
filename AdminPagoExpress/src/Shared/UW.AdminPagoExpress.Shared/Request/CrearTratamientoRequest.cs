namespace UW.AdminPagoExpress.Shared.Request;

public class CrearTratamientoRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string HistoriaClinica { get; set; } = "0";
    public string Estado { get; set; } = "0";
    public string VentaLibre { get; set; } = "0";
    public long IdCategoria { get; set; }

    // Constructor vacío requerido por Blazor
    public CrearTratamientoRequest() { }

    // Constructor de conveniencia (para inicialización)
    public CrearTratamientoRequest(string nombre, string historiaClinica, string estado, string ventaLibre, long idCategoria)
    {
        Nombre = nombre;
        HistoriaClinica = historiaClinica;
        Estado = estado;
        VentaLibre = ventaLibre;
        IdCategoria = idCategoria;
    }
}
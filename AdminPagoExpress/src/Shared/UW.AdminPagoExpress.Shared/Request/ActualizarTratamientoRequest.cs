namespace UW.AdminPagoExpress.Shared.Request;

public class ActualizarTratamientoRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string HistoriaClinica { get; set; }
    public string Estado { get; set; } = "0";
    public string VentaLibre { get; set; }
    public long IdCategoria { get; set; }

    public ActualizarTratamientoRequest() { }

    public ActualizarTratamientoRequest(string nombre, string historiaClinica, string estado, string ventaLibre, long idCategoria)
    {
        Nombre = nombre;
        HistoriaClinica = historiaClinica;
        Estado = estado;
        VentaLibre = ventaLibre;
        IdCategoria = idCategoria;
    }
}
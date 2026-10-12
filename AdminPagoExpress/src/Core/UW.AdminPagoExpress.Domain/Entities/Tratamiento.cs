namespace UW.AdminPagoExpress.Domain.Entities;

public class Tratamiento
{
    public long Id { get; private set; }
    public string Nombre { get; private set; }
    public string? HistoriaClinica { get; private set; }
    public string? Estado { get; private set; }
    public string? VentaLibre { get; private set; }
    public long IdCategoria { get; private set; }

    // Constructor para EF Core
    private Tratamiento() { }

    public Tratamiento(string nombre, string historiaClinica, string estado, string ventaLibre, long idCategoria)
    {
        Nombre = nombre;
        HistoriaClinica = historiaClinica;
        Estado = estado;
        VentaLibre = ventaLibre;
        IdCategoria = idCategoria;
    }

    // Método de fábrica para creación
    public static Tratamiento Crear(string nombre, string historiaClinica, string estado, string ventaLibre, long idCategoria)
    {
        return new Tratamiento(nombre, historiaClinica, estado, ventaLibre, idCategoria);
    }

    // Método para actualización
    public void Actualizar(string nombre, string historiaClinica, string estado, string ventaLibre, long idCategoria)
    {
        Nombre = nombre;
        HistoriaClinica = historiaClinica;
        Estado = estado;
        VentaLibre = ventaLibre;
        IdCategoria = idCategoria;
    }
}
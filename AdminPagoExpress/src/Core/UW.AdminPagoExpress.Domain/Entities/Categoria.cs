namespace UW.AdminPagoExpress.Domain.Entities;

public class Categoria
{
    public long Id { get; private set; }
    public string Nombre { get; private set; }
    // Homogeneizamos Estado con Tratamiento/Curso: '1' = activo, '0' = inactivo
    public string Estado { get; private set; }
    public string? Descripcion { get; private set; }

    private Categoria() { }

    public Categoria(string nombre, string estado, string? descripcion)
    {
        Nombre = nombre;
        Estado = estado;
        Descripcion = descripcion;
    }

    // Método de fábrica con validaciones
    public static Categoria Crear(string nombre, string estado, string? descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));

        if (nombre.Length > 50)
            throw new ArgumentException("El nombre no debe exceder los 50 caracteres.", nameof(nombre));

        if (estado != "1" && estado != "0")
            throw new ArgumentException("Estado inválido. Use '1' para activo o '0' para inactivo.", nameof(estado));

        return new Categoria(nombre.Trim(), estado, descripcion?.Trim());
    }

    // Método para actualización con validaciones
    public void Actualizar(string nombre, string estado, string? descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));

        if (nombre.Length > 50)
            throw new ArgumentException("El nombre no debe exceder los 50 caracteres.", nameof(nombre));

        if (estado != "1" && estado != "0")
            throw new ArgumentException("Estado inválido. Use '1' para activo o '0' para inactivo.", nameof(estado));

        Nombre = nombre.Trim();
        Estado = estado;
        Descripcion = descripcion?.Trim();
    }

    // Métodos auxiliares
    public bool EsActivo() => Estado == "1";
    public void MarcarActivo() => Estado = "1";
    public void MarcarInactivo() => Estado = "0";
}

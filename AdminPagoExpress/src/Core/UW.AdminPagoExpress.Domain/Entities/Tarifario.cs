namespace UW.AdminPagoExpress.Domain.Entities;

public class Tarifario
{
    public long Id { get; private set; }
    public decimal PrecioAlumno { get; private set; }
    public decimal PrecioExterno { get; private set; }
    public decimal PrecioEmpleado { get; private set; }
    public string CodigoSpring { get; private set; }
    public string CodigoOracle { get; private set; }
    public long IdTratamiento { get; private set; }
    public Tratamiento? Tratamiento { get; set; }

    private Tarifario() { }

    public Tarifario(decimal precioAlumno, decimal precioExterno, decimal precioEmpleado, string codigoSpring, string codigoOracle, long idTratamiento)
    {
        PrecioAlumno = precioAlumno;
        PrecioExterno = precioExterno;
        PrecioEmpleado = precioEmpleado;
        CodigoSpring = codigoSpring;
        CodigoOracle = codigoOracle;
        IdTratamiento = idTratamiento;
    }

    public static Tarifario Crear(decimal precioAlumno, decimal precioExterno, decimal precioEmpleado, string codigoSpring, string codigoOracle, long idTratamiento)
    {
        return new Tarifario(precioAlumno, precioExterno, precioEmpleado, codigoSpring, codigoOracle, idTratamiento);
    }

    public void Actualizar(decimal precioAlumno, decimal precioExterno, decimal precioEmpleado, string codigoSpring, string codigoOracle, long idTratamiento)
    {
        PrecioAlumno = precioAlumno;
        PrecioExterno = precioExterno;
        PrecioEmpleado = precioEmpleado;
        CodigoSpring = codigoSpring;
        CodigoOracle = codigoOracle;
        IdTratamiento = idTratamiento;
    }
}

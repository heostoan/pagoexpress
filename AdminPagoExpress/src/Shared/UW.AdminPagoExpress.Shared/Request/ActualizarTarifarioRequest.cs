namespace UW.AdminPagoExpress.Shared.Request;

public class ActualizarTarifarioRequest
{
    public decimal PrecioAlumno { get; set; }
    public decimal PrecioExterno { get; set; }
    public decimal PrecioEmpleado { get; set; }
    public string CodigoSpring { get; set; } = string.Empty;
    public string CodigoOracle { get; set; } = string.Empty;
    public long IdTratamiento { get; set; }

    public ActualizarTarifarioRequest() { }

    public ActualizarTarifarioRequest(decimal precioAlumno, decimal precioExterno, decimal precioEmpleado, string codigoSpring, string codigoOracle, long idTratamiento)
    {
        PrecioAlumno = precioAlumno;
        PrecioExterno = precioExterno;
        PrecioEmpleado = precioEmpleado;
        CodigoSpring = codigoSpring;
        CodigoOracle = codigoOracle;
        IdTratamiento = idTratamiento;
    }
}

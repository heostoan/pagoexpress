namespace UW.AdminPagoExpress.Shared.Response;

public record TarifarioResponse(
    long Id,
    decimal PrecioAlumno,
    decimal PrecioExterno,
    decimal PrecioEmpleado,
    string CodigoSpring,
    string CodigoOracle,
    long IdTratamiento
);

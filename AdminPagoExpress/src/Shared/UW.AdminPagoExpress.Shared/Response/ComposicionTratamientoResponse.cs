namespace UW.AdminPagoExpress.Shared.Response;

public record ComposicionTratamientoResponse(
    long IdComposicion,
    long IdTratamiento,
    string CodigoOracle,
    decimal Cantidad,
    string UnidadMedida
);

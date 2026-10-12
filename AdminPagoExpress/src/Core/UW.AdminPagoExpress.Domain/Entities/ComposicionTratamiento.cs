namespace UW.AdminPagoExpress.Domain.Entities;

public class ComposicionTratamiento
{
    public long IdComposicionTratamiento { get; private set; }
    public long IdTratamiento { get; private set; }
    public string CodigoOracle { get; private set; } = string.Empty;
    public decimal Cantidad { get; private set; }
    public string UnidadMedida { get; private set; } = string.Empty;

    private ComposicionTratamiento() { }

    public ComposicionTratamiento(long idTratamiento, string codigoOracle, decimal cantidad, string unidadMedida)
    {
        IdTratamiento = idTratamiento;
        CodigoOracle = codigoOracle;
        Cantidad = cantidad;
        UnidadMedida = unidadMedida;
    }

    public static ComposicionTratamiento Crear(long idTratamiento, string codigoOracle, decimal cantidad, string unidadMedida)
    {
        return new ComposicionTratamiento(idTratamiento, codigoOracle, cantidad, unidadMedida);
    }

    public void Actualizar(string codigoOracle, decimal cantidad, string unidadMedida)
    {
        CodigoOracle = codigoOracle;
        Cantidad = cantidad;
        UnidadMedida = unidadMedida;
    }
}

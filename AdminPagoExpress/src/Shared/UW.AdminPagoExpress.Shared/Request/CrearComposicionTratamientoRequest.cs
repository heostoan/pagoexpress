namespace UW.AdminPagoExpress.Shared.Request;

public class CrearComposicionTratamientoRequest
{
    public CrearComposicionTratamientoRequest() { }

    // IdTratamiento padre y lista de composiciones (cada item con IdTratamientoRelacionado, CodigoOracle, Cantidad, UnidadMedida)
    public long IdTratamiento { get; set; }
    public List<ComposicionItemRequest> Items { get; set; } = new();
}

public class ComposicionItemRequest
{
    public long IdTratamiento { get; set; }
    public string CodigoOracle { get; set; } = string.Empty;
    public decimal Cantidad { get; set; } = 1;
    public string UnidadMedida { get; set; } = string.Empty;
}

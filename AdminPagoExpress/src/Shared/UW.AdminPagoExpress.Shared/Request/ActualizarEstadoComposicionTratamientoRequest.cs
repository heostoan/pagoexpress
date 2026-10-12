namespace UW.AdminPagoExpress.Shared.Request;

public class ActualizarEstadoComposicionTratamientoRequest
{
    public ActualizarEstadoComposicionTratamientoRequest() { }
    public ActualizarEstadoComposicionTratamientoRequest(long idComposicion, int estado)
    {
        IdComposicion = idComposicion;
        Estado = estado;
    }

    public long IdComposicion { get; set; }
    public int Estado { get; set; }
}

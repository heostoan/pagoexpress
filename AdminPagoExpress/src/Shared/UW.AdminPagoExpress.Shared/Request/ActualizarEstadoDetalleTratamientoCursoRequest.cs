namespace UW.AdminPagoExpress.Shared.Request;

public class ActualizarEstadoDetalleTratamientoCursoRequest
{
    public ActualizarEstadoDetalleTratamientoCursoRequest() { }

    public ActualizarEstadoDetalleTratamientoCursoRequest(long idDetalle, int estado)
    {
        IdDetalle = idDetalle;
        Estado = estado;
    }

    public long IdDetalle { get; set; }
    public int Estado { get; set; }
}

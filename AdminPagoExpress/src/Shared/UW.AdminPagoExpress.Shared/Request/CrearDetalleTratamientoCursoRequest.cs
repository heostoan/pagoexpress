namespace UW.AdminPagoExpress.Shared.Request;

public class CrearDetalleTratamientoCursoRequest
{
    public CrearDetalleTratamientoCursoRequest() { }

    public CrearDetalleTratamientoCursoRequest(long idCurso, List<long> idTratamientos)
    {
        IdCurso = idCurso;
        IdTratamientos = idTratamientos;
    }

    public long IdCurso { get; set; }
    public List<long> IdTratamientos { get; set; } = new();
}

namespace UW.AdminPagoExpress.Domain.Entities;

public class DetalleTratamientoCurso
{
    public long IdDetalleTratamientoCurso { get; private set; }
    public long IdTratamiento { get; private set; }
    public long IdCurso { get; private set; }
    public int Estado { get; private set; } // 1 activo, 0 inactivo

    private DetalleTratamientoCurso() { }

    public DetalleTratamientoCurso(long idTratamiento, long idCurso, int estado = 1)
    {
        IdTratamiento = idTratamiento;
        IdCurso = idCurso;
        Estado = estado;
    }

    public static DetalleTratamientoCurso Crear(long idTratamiento, long idCurso)
    {
        return new DetalleTratamientoCurso(idTratamiento, idCurso, 1);
    }

    public void CambiarEstado(int estado)
    {
        Estado = estado;
    }
}

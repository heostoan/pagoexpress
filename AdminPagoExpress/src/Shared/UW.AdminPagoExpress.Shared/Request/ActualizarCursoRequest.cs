namespace UW.AdminPagoExpress.Shared.Request;

public class ActualizarCursoRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string CodigoCurso { get; set; } = string.Empty;
    public string Ciclo { get; set; } = string.Empty;
    public string Sis { get; set; } = string.Empty;
    public string Estado { get; set; } = "0";

    public ActualizarCursoRequest() { }

    public ActualizarCursoRequest(string nombre, string codigoCurso, string ciclo, string sis, string estado)
    {
        Nombre = nombre;
        CodigoCurso = codigoCurso;
        Ciclo = ciclo;
        Sis = sis;
        Estado = estado;
    }
}

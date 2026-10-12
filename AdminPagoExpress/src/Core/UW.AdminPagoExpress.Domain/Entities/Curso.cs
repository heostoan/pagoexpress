namespace UW.AdminPagoExpress.Domain.Entities;

public class Curso
{
    public long Id { get; private set; }
    public string Nombre { get; private set; }
    public string CodigoCurso { get; private set; }
    public string Ciclo { get; private set; }
    public string Sis { get; private set; }
    public string Estado { get; private set; }

    private Curso() { } // Constructor para EF 

    public Curso(string nombre, string codigoCurso, string ciclo, string sis, string estado)
    {
        Nombre = nombre;
        CodigoCurso = codigoCurso;
        Ciclo = ciclo;
        Sis = sis;
        Estado = estado;
    }

    public static Curso Crear(string nombre, string codigoCurso, string ciclo, string sis, string estado)
    {
        return new Curso(nombre, codigoCurso, ciclo, sis, estado);
    }

    public void Actualizar(string nombre, string codigoCurso, string ciclo, string sis, string estado)
    {
        Nombre = nombre;
        CodigoCurso = codigoCurso;
        Ciclo = ciclo;
        Sis = sis;
        Estado = estado;
    }
}

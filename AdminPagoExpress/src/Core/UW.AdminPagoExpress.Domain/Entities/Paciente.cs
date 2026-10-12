namespace UW.AdminPagoExpress.Domain.Entities;

public class Paciente
{
    public long Id { get; private set; }
    public string TipoDocumento { get; private set; }
    public string DocIdentidad { get; private set; }
    public string Nombres { get; private set; }
    public string ApellidoPaterno { get; private set; }
    public string ApellidoMaterno { get; private set; }
    public string Genero { get; private set; }
    public string Celular { get; private set; }
    public string Correo { get; private set; }
    public DateTime FechaNacimiento { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime FechaModificacion { get; private set; }
    public long IdAlumno { get; private set; }
    public long IdFirma { get; private set; }

    private Paciente() { }

    public Paciente(string tipoDocumento, string docIdentidad, string nombres, string apellidoPaterno, string apellidoMaterno, string genero, string celular, string correo, DateTime fechaNacimiento, long idAlumno, long idFirma)
    {
        TipoDocumento = tipoDocumento;
        DocIdentidad = docIdentidad;
        Nombres = nombres;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        Genero = genero;
        Celular = celular;
        Correo = correo;
        FechaNacimiento = fechaNacimiento;
        FechaCreacion = DateTime.UtcNow;
        FechaModificacion = DateTime.UtcNow;
        IdAlumno = idAlumno;
        IdFirma = idFirma;
    }

    public static Paciente Crear(string tipoDocumento, string docIdentidad, string nombres, string apellidoPaterno, string apellidoMaterno, string genero, string celular, string correo, DateTime fechaNacimiento, long idAlumno, long idFirma)
    {
        return new Paciente(tipoDocumento, docIdentidad, nombres, apellidoPaterno, apellidoMaterno, genero, celular, correo, fechaNacimiento, idAlumno, idFirma);
    }

    public void Actualizar(string tipoDocumento, string docIdentidad, string nombres, string apellidoPaterno, string apellidoMaterno, string genero, string celular, string correo, DateTime fechaNacimiento, long idAlumno, long idFirma)
    {
        TipoDocumento = tipoDocumento;
        DocIdentidad = docIdentidad;
        Nombres = nombres;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        Genero = genero;
        Celular = celular;
        Correo = correo;
        FechaNacimiento = fechaNacimiento;
        FechaModificacion = DateTime.UtcNow;
        IdAlumno = idAlumno;
        IdFirma = idFirma;
    }
}

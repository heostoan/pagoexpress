namespace UW.AdminPagoExpress.Shared.Dtos.Alumno;

public class AlumnoDto
{
    public string CodigoAlumno { get; set; }
    public string Nombres { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public string Correo { get; set; }
    public List<CursoDto> Cursos { get; set; }
}

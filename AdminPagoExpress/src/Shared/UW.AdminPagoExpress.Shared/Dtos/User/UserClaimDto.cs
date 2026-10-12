namespace UW.AdminPagoExpress.Shared.Dtos.User;

public class UserClaimDto
{
    public string Nombres { get; set; }
    public string TipoUsuario { get; set; }
    public string Usuario { get; set; }
    public string CodigoAlumno { get; set; }
    public string IdSesion { get; set; }
    public string Carrera { get; set; }
    public string Institucion { get; set; }
    public string CarreraDescripcion { get; set; }
    public string Periodo { get; set; }
    public string PeriodoDescripcion { get; set; }
    public string Mes { get; set; }
    public string Ip { get; set; }
    public int PKCodigo { get; set; }
    public string Opcion { get; set; }
    public string Grado { get; set; }
    public bool MatriculaExtemporanea { get; set; }
    public string FechaFinMatricula { get; set; }
    public bool MatriculaIntegral { get; set; }
    public string MatriculaIntegralCodigoUsuario { get; set; }

    public bool Matriculado { get; set; }
    public string CorreoInstitucional { get; set; }
    public string CorreoPersonal { get; set; }
    public string IpUsuario { get; set; }
    public decimal AlumnoPromedio { get; set; }
}
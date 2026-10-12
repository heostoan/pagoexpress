namespace UW.AdminPagoExpress.Shared.Endpoints;

public static class MatriculaEndpoints
{
    public const string MatriculaObtenerCursoHabilThreads = "/api/matricula/obtener-curso-habilThreads";

    public const string MatriculaListarCredito = "/api/matricula/listar-credito";

    //public const string MatriculaValidaCondicion = "/api/matricula/valida-matricula";
    public const string MatriculaObtenerDatosParaGrabarCurso = "/api/matricula/obtener-datos-para-grabar-curso";
    public const string MatriculaInsertarCursoSinValidacionMatricula = "/api/matricula/insertar-curso-matricula";
    public const string MatriculaValidacionInsertarCurso = "/api/matricula/validacion-insertar-curso";

    public const string MatriculaValidacionInsertarCursoMatricula =
        "/api/matricula/validacion-insertar-curso-matricula";

    public const string MatriculaEliminarCurso = "/api/matricula/eliminar-curso";
    public const string MatriculaEliminarMatricula = "/api/matricula/eliminar-matricula";
    public const string MatriculaEliminarCursoMatricula = "/api/matricula/eliminar-curso-matricula";

    public const string MatriculaTiempoSesion = "/api/matricula/tiempo-sesion";

    public const string MatriculaValidarProcesoMatricula = "/api/matricula/validar-proceso-matricula";
    public const string MatriculaProcesoMatricula = "/api/matricula/insertar-proceso-matricula";

    public const string MatriculaProcesoMatriculaExtemporanea =
        "/api/matricula/insertar-proceso-matricula-extemporanea";

    public const string MatriculaValidarPagoAlumno = "api/matricula/validar-pago-alumno";

    //MATRICULA LAZY LOADING
    public const string MatriculaObtenerSoloCursoHabil = "/api/matricula/obtener-solo-curso-habil";
    public const string MatriculaObtenerSoloCursoSeccionHabil = "/api/matricula/obtener-solo-curso-seccion-habil";

    public const string MatriculaObtenerSoloCursoSeccionGrupoHabil =
        "/api/matricula/obtener-solo-curso-seccion-grupo-habil";
    //public const string MatriculaParametrosMatricula = "/api/matricula/parametros-matricula";
}
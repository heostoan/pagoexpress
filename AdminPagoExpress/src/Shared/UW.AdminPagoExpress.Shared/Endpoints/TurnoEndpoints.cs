namespace UW.AdminPagoExpress.Shared.Endpoints;

public static class TurnoEndpoints
{
    public const string TurnoListarCronograma = "/api/turno/listar-cronograma";
    public const string TurnoObtenerCronogramaMatriculaAlumno = "/api/turno/obtener-cronograma-matricula-alumno";
    public const string TurnoCreateMatriculaConsideraciones = "/api/turno/CreateMatriculaConsideraciones";
    public const string TurnoGetVerificaPago = "/api/turno/VerificaPago";
    public const string TurnoCreateAceptaMatricula = "/api/turno/CreateAceptaMatricula";
    public const string TurnoObtenerPlanAlumnoMatricula = "/api/turno/obtener-plan-alumno-matricula";

    public const string TurnoValidarCondicion = "api/turno/Validar-condicion";
    public const string GetEarlistCronograma = "api/turno/earlistCronograma";
}
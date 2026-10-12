namespace UW.AdminPagoExpress.Shared.Endpoints;

public static class SolicitudSeccionAdicionalEndpoints
{
    public const string SolicitudSeccionAdicionalListar = "/api/matricula/listar-seccionesAdicionales";

    public const string SolicitudSeccionAdicionalListarMatriculadoPorAlumno =
        "/api/matricula/listar-seccionesAdicionalesMatriculadosPorAlumno";

    public const string SolicitudSeccionAdicionalGuardar = "/api/matricula/guardar-seccionesAdicionales";
    public const string SolicitudSeccionAdicionalActualizar = "/api/matricula/actualizar-seccionesAdicionales";

    public const string SolicitudSeccionAdicionalActivo = "/api/matricula/secciones-adicionales-activo";
	public const string SolicitudSeccionAdicionalGetFechaExpiracionSeccionesAdicionales = "/api/matricula/get-fechaexpiracion-seccionesadicionales";
    public const string SolicitudSeccionAdicionalListarEstados = "/api/cursosadicionales/listar-estados";
    public const string SolicitudSeccionAdicionalListarSubEstados = "/api/cursosadicionales/listar-subestados";
    public const string SolicitudSeccionAdicionalDetalleSeguimieto = "/api/cursosadicionales/listar-seguimiento";
    public const string SolicitudSeccionAdicionalCambiarEstado = "/api/cursosadicionales/cambiar-estado";
}
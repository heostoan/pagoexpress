namespace UW.AdminPagoExpress.Shared.Response;

public record DetalleTratamientoCursoResponse(
    long IdDetalle,
    long IdTratamiento,
    long IdCurso,
    int Estado
);

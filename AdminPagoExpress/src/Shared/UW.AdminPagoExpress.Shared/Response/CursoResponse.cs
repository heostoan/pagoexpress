namespace UW.AdminPagoExpress.Shared.Response;

public record CursoResponse(
    long Id,
    string Nombre,
    string CodigoCurso,
    string Ciclo,
    string Sis,
    string Estado
);

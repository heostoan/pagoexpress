namespace UW.AdminPagoExpress.Shared.Response;

public record CategoriaResponse(
    long Id,
    string Nombre,
    string Estado,
    string? Descripcion
);

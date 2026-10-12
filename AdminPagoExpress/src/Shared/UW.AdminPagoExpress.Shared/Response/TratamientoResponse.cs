namespace UW.AdminPagoExpress.Shared.Response;

public record TratamientoResponse(
    long Id,
    string Nombre,
    string HistoriaClinica,
    string Estado,
    string VentaLibre,
    long IdCategoria,
    string NombreCategoria
);
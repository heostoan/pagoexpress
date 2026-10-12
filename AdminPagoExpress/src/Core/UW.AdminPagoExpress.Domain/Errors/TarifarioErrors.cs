using UW.AdminPagoExpress.Domain.Common;

namespace UW.AdminPagoExpress.Domain.Errors;

public static class TarifarioErrors
{
    public static readonly Error NoEncontrado = new(
        "Tarifario.NoEncontrado",
        "El tarifario solicitado no existe.");

    public static readonly Error CodigoDuplicado = new(
        "Tarifario.CodigoDuplicado",
        "Ya existe un tarifario con el mismo código.");
}

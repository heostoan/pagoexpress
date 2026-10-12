using UW.AdminPagoExpress.Domain.Common;

namespace UW.AdminPagoExpress.Domain.Errors;

public static class TratamientoErrors
{
    public static readonly Error NombreDuplicado = new(
        "Tratamiento.NombreDuplicado",
        "Ya existe un tratamiento con el mismo nombre.");

    public static readonly Error NoEncontrado = new(
        "Tratamiento.NoEncontrado",
        "El tratamiento solicitado no existe.");
}
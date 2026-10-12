using UW.AdminPagoExpress.Domain.Common;

namespace UW.AdminPagoExpress.Domain.Errors;

public static class CursoErrors
{
    public static readonly Error NombreDuplicado = new(
        "Curso.NombreDuplicado",
        "Ya existe un curso con el mismo nombre.");

    public static readonly Error NoEncontrado = new(
        "Curso.NoEncontrado",
        "El curso solicitado no existe.");
}

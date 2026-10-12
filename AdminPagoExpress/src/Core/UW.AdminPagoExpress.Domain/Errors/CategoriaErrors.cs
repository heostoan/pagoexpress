using UW.AdminPagoExpress.Domain.Common;

namespace UW.AdminPagoExpress.Domain.Errors;

public static class CategoriaErrors
{
    public static readonly Error NombreDuplicado = new(
        "Categoria.NombreDuplicado",
        "Ya existe una categoría con el mismo nombre.");

    public static readonly Error NoEncontrada = new(
        "Categoria.NoEncontrada",
        "La categoría solicitada no existe.");
}
namespace UW.AdminPagoExpress.Shared.Request;

public class UpdateCategoryRequest
{
    public UpdateCategoryRequest()
    {
    }

    public UpdateCategoryRequest(int idCategoria, string? nombre)
    {
        IdCategoria = idCategoria;
        Nombre = nombre;
    }

    public int IdCategoria { get; set; }
    public string? Nombre { get; set; }
}

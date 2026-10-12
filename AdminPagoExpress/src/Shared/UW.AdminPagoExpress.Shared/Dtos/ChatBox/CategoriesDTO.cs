namespace UW.AdminPagoExpress.Shared.Dtos.ChatBox;

public class CategoriesDTO
{
    public int IdCategoria { get; set; }
    public string? Modalidad { get; set; }
    public string? Nombre { get; set; }
    public bool HasChildren { get; set; } = false;

    public List<CategoriesDTO> Children { get; set; } = new List<CategoriesDTO>();
}

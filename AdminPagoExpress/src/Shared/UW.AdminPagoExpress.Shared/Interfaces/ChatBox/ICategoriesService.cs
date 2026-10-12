using UW.AdminPagoExpress.Shared.Dtos;
using UW.AdminPagoExpress.Shared.Dtos.ChatBox;
using UW.AdminPagoExpress.Shared.Request;
namespace UW.AdminPagoExpress.Shared.Interfaces.ChatBox;

public interface ICategoriesService
{
    Task<IEnumerable<CategoriesDTO>> GetCategoriesAsync();
    Task<CategoriesDTO> GetCategoryAsync(int Id);
    Task<MensajeResponseDto> UpdateCategoryAsync(UpdateCategoryRequest request);
    Task<MensajeResponseDto> DeleteCategoryAsync(int Id);
}

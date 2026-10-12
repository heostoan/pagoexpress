using UW.AdminPagoExpress.Shared.Dtos.Alumno;
using UW.AdminPagoExpress.Shared.Dtos.Pagination;

namespace UW.AdminPagoExpress.Shared.Interfaces;

public interface IAlumnoService
{
    Task<PagedResult<AlumnoDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<AlumnoDto?> GetByIdAsync(string codigoAlumno);
    Task<AlumnoDto> CreateAsync(AlumnoDto alumno);
    Task<bool> UpdateAsync(string codigoAlumno, AlumnoDto alumno);
    Task<bool> DeleteAsync(string codigoAlumno);
}

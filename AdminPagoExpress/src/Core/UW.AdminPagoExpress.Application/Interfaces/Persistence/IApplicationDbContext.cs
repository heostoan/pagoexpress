using Microsoft.EntityFrameworkCore;
using UW.AdminPagoExpress.Domain.Entities;

namespace UW.AdminPagoExpress.Application.Interfaces.Persistence;

public interface IApplicationDbContext
{
    DbSet<Tratamiento> Tratamientos { get; }
    DbSet<Categoria> Categorias { get; }
    DbSet<Curso> Cursos { get; }
    DbSet<Tarifario> Tarifarios { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
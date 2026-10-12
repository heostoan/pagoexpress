using Microsoft.EntityFrameworkCore;
using UW.AdminPagoExpress.Application.Interfaces.Persistence;

namespace UW.AdminPagoExpress.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Tratamiento> Tratamientos => Set<Tratamiento>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Tarifario> Tarifarios => Set<Tarifario>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<DetalleTratamientoCurso> DetalleTratamientoCurso => Set<DetalleTratamientoCurso>();
    public DbSet<ComposicionTratamiento> ComposicionTratamientos => Set<ComposicionTratamiento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
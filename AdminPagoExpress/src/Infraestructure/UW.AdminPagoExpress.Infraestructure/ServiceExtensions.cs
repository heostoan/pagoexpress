using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Domain.Helpers;
using UW.AdminPagoExpress.Infrastructure.Services;
using UW.AdminPagoExpress.Infrastructure.Persistence;
using UW.AdminPagoExpress.Infrastructure.Repositories;

namespace UW.AdminPagoExpress.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories(configuration);
        services.AddServices();
        return services;
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        // Mantener DbContext e infraestructura mínima necesaria para EF
    }

    private static void AddRepositories(this IServiceCollection services, IConfiguration config)
    {
        // Repositorios EF de las entidades conservadas
        services.AddScoped<Application.Interfaces.Persistence.IUnitOfWork, EfUnitOfWork>();

        services.AddScoped<ITratamientoRepository, TratamientoRepository>();
        services.AddScoped<ICursoRepository, CursoRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<ITarifarioRepository, TarifarioRepository>();
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IDetalleTratamientoCursoRepository, DetalleTratamientoCursoRepository>();
        services.AddScoped<IComposicionTratamientoRepository, ComposicionTratamientoRepository>();

        // Registrar implementaciones nulas para repositorios antiguos eliminados para mantener validación DI
        // No registrar implementaciones provisionales; las features no relacionadas deben eliminarse en Application si procede.
    }

    private static void AddServices(this IServiceCollection services)
    {
        // Servicios mínimos requeridos por la aplicación
        services.AddSingleton<ILogEventSink, OracleLogSink>();
        services.AddSingleton<UrlCrypto>();
    }
}
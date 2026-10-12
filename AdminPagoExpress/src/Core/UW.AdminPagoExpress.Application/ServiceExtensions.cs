using System.Reflection;
using UW.AdminPagoExpress.Domain.Helpers;

namespace UW.AdminPagoExpress.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddFeatures();
        services.AddRepositories();
        return services;
    }

    private static void AddFeatures(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var config = TypeAdapterConfig.GlobalSettings;
        // 🔍 Esto es CLAVE: escanea todos los IRegister
        config.Scan(AppDomain.CurrentDomain.GetAssemblies());

        // 🧠 Registrar en DI

        // 2. Register Mapster services
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<UrlCrypto>();
    }
}
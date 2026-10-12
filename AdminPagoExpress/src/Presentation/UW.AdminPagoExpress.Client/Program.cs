using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UW.AdminPagoExpress.Client.Services;
using UW.AdminPagoExpress.Shared.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddTransient(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});


builder.Services.AddTransient<ITratamientoApiClient>(sp =>
{
    var http = sp.GetRequiredService<HttpClient>();
    return new TratamientoApiClient(http);
});

builder.Services.AddTransient<ICursoApiClient>(sp =>
{
    var http = sp.GetRequiredService<HttpClient>();
    return new CursoApiClient(http);
});

builder.Services.AddTransient<ICategoriaApiClient>(sp =>
{
    var http = sp.GetRequiredService<HttpClient>();
    return new CategoriaApiClient(http);
});

builder.Services.AddTransient<ITarifarioApiClient>(sp =>
{
    var http = sp.GetRequiredService<HttpClient>();
    return new TarifarioApiClient(http);
});

builder.Services.AddTransient<IPacienteApiClient>(sp =>
{
    var http = sp.GetRequiredService<HttpClient>();
    return new PacienteApiClient(http);
});

builder.Services.AddTransient<IDetalleTratamientoCursoApiClient>(sp =>
{
    var http = sp.GetRequiredService<HttpClient>();
    return new DetalleTratamientoCursoApiClient(http);
});

builder.Services.AddTransient<IComposicionTratamientoApiClient>(sp =>
{
    var http = sp.GetRequiredService<HttpClient>();
    return new ComposicionTratamientoApiClient(http);
});

builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();
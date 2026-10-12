using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Tokens;
using Oracle.ManagedDataAccess.Client;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Text;
using UW.AdminPagoExpress.Application;
using UW.AdminPagoExpress.Application.Common;
using UW.AdminPagoExpress.Components;
using UW.AdminPagoExpress.Endpoints;
using UW.AdminPagoExpress.Infrastructure;
using UW.AdminPagoExpress.Infrastructure.Persistence;
using UW.AdminPagoExpress.Services;
using UW.AdminPagoExpress.Shared.Dtos;
using UW.AdminPagoExpress.Shared.Interfaces;
using UW.AdminPagoExpress.Shared.Interfaces.ChatBox;
using _Imports = UW.AdminPagoExpress.Client._Imports;
using WebApplication = Microsoft.AspNetCore.Builder.WebApplication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDbConnectionProduccion")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Registros mínimos de servicios necesarios
builder.Services.AddScoped<IUrlCryptoService, UrlCryptoService>();
builder.Services.AddTransient<IHttpContextService, HttpContextService>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddMemoryCache();

var nombre = builder.Configuration.GetSection("HealthCheck:HealthCheckNombre").Get<string>();
var tags = builder.Configuration.GetSection("HealthCheck:HealthCheckTags").Get<string[]>();
var healthCheckUri = builder.Configuration.GetSection("HealthCheck:HealthCheckUri").Get<string>();

builder.Services.AddHealthChecks().AddOracle(
    builder.Configuration.GetConnectionString("OracleDbConnectionProduccion"),
    name: nombre,
    failureStatus: HealthStatus.Unhealthy, // Estado si falla
    tags: tags); // Etiquetas para filtrar;


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ProtectedLocalStorage>();
//Add extensions
builder.Services.AddControllersWithViews();
builder.Services.AddApplication()
    .AddInfraestructure(builder.Configuration);

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Services.AddResponseCompression(options => { options.EnableForHttps = true; });

builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();
builder.Services.AddAuthentication(options =>
    {
        // Define el esquema predeterminado como cookies
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme =
            CookieAuthenticationDefaults.AuthenticationScheme; // Este esquema se usar� para desafiar la autenticaci�n
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    })
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddMicrosoftGraph()
    .AddInMemoryTokenCaches();

// Configuraci�n del esquema de cookies
builder.Services.Configure<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.Name = "MyAppCookie"; // Nombre personalizado de la cookie
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Solo usar cookies seguras
    options.LoginPath = "/login"; // P�gina personalizada de login (si la tienes)
    options.LogoutPath = "/auth/Logout"; // P�gina personalizada de logout (si la tienes)
});

builder.Host.UseSerilog((context, services, configuration) =>
{
    var logSink = services.GetRequiredService<ILogEventSink>();

    configuration
        .MinimumLevel.Information() // Permite todos los logs de nivel Information y superior
        .WriteTo.Sink(logSink) // Usa el logSink personalizado para guardar los logs

        // Filtra los logs autom�ticos de ASP.NET Core
        .Filter.ByExcluding(logEvent =>
            logEvent.Properties.ContainsKey("SourceContext") &&
            logEvent.Properties["SourceContext"].ToString().Contains("Microsoft"));

    // Establecemos que NO se registren logs de ASP.NET Core ni de System
    // Establece el nivel de log m�s alto (Fatal) para que se ignoren
    configuration
        .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal) // Ignora todos los logs de Microsoft
        .MinimumLevel.Override("System", LogEventLevel.Fatal); // Ignora todos los logs de System
});

builder.Services.Configure<EnvioCorreoConfigDto>(builder.Configuration.GetSection("EnvioCorreo"));

builder.Services.AddAuthorizationCore();

builder.Services.AddSignalR(options =>
{
    options.MaximumReceiveMessageSize = 102400; // 32 KB (ajusta según tus necesidades)
    options.EnableDetailedErrors = true; // Habilita errores detallados para depuración
    options.KeepAliveInterval = TimeSpan.FromSeconds(15); // Intervalo de keep-alive
    options.HandshakeTimeout = TimeSpan.FromSeconds(30);
    options.ClientTimeoutInterval = TimeSpan.FromMinutes(30); // Tiempo de espera del cliente
});
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxConcurrentConnections = 5000;
    options.Limits.MaxConcurrentUpgradedConnections = 5000;
    options.Limits.MaxRequestBodySize = 30 * 1024 * 1024; // 30 MB
});
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.Http2.InitialConnectionWindowSize = 65535; // Ejemplo de configuración
    // No hay una opción directa para desactivar la compresión de WebSocket aquí
});

// Configurar logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Agrega logs a la consola
builder.Logging.AddDebug(); // Agrega logs al depurador


// Configuración Oracle GLOBAL
OracleConfiguration.TnsAdmin =
    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Base\\Wallet");

var app = builder.Build();

app.MapGet("/nombreServidor", () => "¡ Servidor 214!");

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Keep-Alive", "timeout=300, max=5000");
    await next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapHealthChecks($"/{healthCheckUri}", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseRouting();


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthentication()
    .UseAuthorization();

app.UseAntiforgery();
app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(_Imports).Assembly);

app.MapTratamientoEndpoints();
app.MapCursoEndpoints();
app.MapCategoriaEndpoints();
app.MapTarifarioEndpoints();
app.MapPacienteEndpoints();
app.MapDetalleTratamientoCursoEndpoints();
app.MapComposicionTratamientoEndpoints();

app.Run();
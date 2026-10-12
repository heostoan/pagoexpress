using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;
using UW.AdminPagoExpress.Domain.Interfaces.Persistance;
using UW.AdminPagoExpress.Shared.Interfaces;

namespace UW.AdminPagoExpress.Infrastructure.Services;

public class OracleLogSink : ILogEventSink
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHttpContextService _httpContextService;

    public OracleLogSink(IServiceScopeFactory scopeFactory, IHttpContextService httpContextService)
    {
        _scopeFactory = scopeFactory;
        _httpContextService = httpContextService;
    }


    public void Emit(LogEvent logEvent)
    {
        using var scope = _scopeFactory.CreateScope();
        // IDbConnectionFactoryManager implementation may have been removed in aggressive cleanup.
        var dbConnectionFactory = scope.ServiceProvider.GetService<IDbConnectionFactoryManager>();
        var HttpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();

        if (dbConnectionFactory == null)
        {
            // Infraestructura DB para logs no disponible: omitir persistencia para evitar fallos durante la limpieza.
            return;
        }

        using var connection = dbConnectionFactory.CreateConnection("OracleDbConnectionMatricula");

        var sql = @"
            INSERT INTO UP100.LOG_MATRICULA (
                ID_SESION,
                USUARIO, 
                NIVEL, 
                MENSAJE, 
                FECHA,
                USUARIOADMIN
            ) VALUES (
                :IdSesion, 
                :Usuario, 
                :Nivel, 
                :Mensaje, 
                SYSTIMESTAMP,
                :UsuarioAdmin
            )";

        var idSesion = logEvent.Properties.ContainsKey("IdSesion")
            ? (logEvent.Properties["IdSesion"] as ScalarValue)?.Value?.ToString() ?? "Desconocido"
            : "Desconocido";
        if (idSesion == "") idSesion = "Desconocido";

        var usuarioAdmin = HttpContextAccessor.HttpContext?.User?.FindFirst("MatriculaIntegralCodigoUsuario")?.Value;


        if (logEvent.Properties.TryGetValue("UsuarioAdmin", out var usuarioAdminValue) &&
            usuarioAdminValue is ScalarValue scalarValue)
            usuarioAdmin = scalarValue.Value?.ToString();

        var mensaje = "";
        if (HttpContextAccessor != null && HttpContextAccessor.HttpContext != null)
        {
            //var ip = HttpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault()
            //         ?? HttpContextAccessor.HttpContext?.Request.Headers["X-Real-IP"].FirstOrDefault()
            //         ?? HttpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

            var ip = _httpContextService.ObtenerIpReal();

            mensaje = ip + " - " + logEvent.MessageTemplate.Render(logEvent.Properties);
        }
        else
        {
            mensaje = logEvent.MessageTemplate.Render(logEvent.Properties);
        }

        var parameters = new
        {
            IdSesion = idSesion,
            Usuario = logEvent.Properties.ContainsKey("Usuario")
                ? (logEvent.Properties["Usuario"] as ScalarValue)?.Value?.ToString() ?? "Desconocido"
                : "Desconocido",
            Nivel = logEvent.Level.ToString(),
            Mensaje = mensaje,
            UsuarioAdmin = usuarioAdmin
        };


        connection.Execute(sql, parameters);
    }






}
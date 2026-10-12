using UW.AdminPagoExpress.Application.Features.Tarifarios.Commands.ActualizarTarifario;
using UW.AdminPagoExpress.Application.Features.Tarifarios.Commands.CrearTarifario;
using UW.AdminPagoExpress.Application.Features.Tarifarios.Commands.EliminarTarifario;
using UW.AdminPagoExpress.Application.Features.Tarifarios.Queries.ListarTarifarios;
using UW.AdminPagoExpress.Application.Features.Tarifarios.Queries.ObtenerTarifarioPorId;
using UW.AdminPagoExpress.Shared.Request;

namespace UW.AdminPagoExpress.Endpoints;

public static class TarifarioEndpoints
{
    public static void MapTarifarioEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/tarifarios").WithTags("Tarifarios");

        group.MapGet("/", async (IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ListarTarifariosQuery(), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });

        group.MapGet("/{id:long}", async (long id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ObtenerTarifarioPorIdQuery(id), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
        });

        group.MapPost("/", async (CrearTarifarioRequest request, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new CrearTarifarioCommand(
                request.PrecioAlumno,
                request.PrecioExterno,
                request.PrecioEmpleado,
                request.CodigoSpring,
                request.CodigoOracle,
                request.IdTratamiento), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.Conflict(result.Error);
        });

        group.MapPut("/{id:long}", async (long id, ActualizarTarifarioRequest request, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ActualizarTarifarioCommand(
                id,
                request.PrecioAlumno,
                request.PrecioExterno,
                request.PrecioEmpleado,
                request.CodigoSpring,
                request.CodigoOracle,
                request.IdTratamiento), ct);
            return result.IsSuccess ? Results.Ok() : Results.Conflict(result.Error);
        });

        group.MapDelete("/{id:long}", async (long id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new EliminarTarifarioCommand(id), ct);
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });
    }
}

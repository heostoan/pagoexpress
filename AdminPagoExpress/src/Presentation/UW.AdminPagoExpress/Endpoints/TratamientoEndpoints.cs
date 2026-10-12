using UW.AdminPagoExpress.Application.Features.Tratamientos.Commands.ActualizarTratamiento;
using UW.AdminPagoExpress.Application.Features.Tratamientos.Commands.CrearTratamiento;
using UW.AdminPagoExpress.Application.Features.Tratamientos.Commands.EliminarTratamiento;
using UW.AdminPagoExpress.Application.Features.Tratamientos.Queries.ListarTratamientos;
using UW.AdminPagoExpress.Application.Features.Tratamientos.Queries.ObtenerTratamientoPorId;
using UW.AdminPagoExpress.Shared.Request;

namespace UW.AdminPagoExpress.Endpoints;

public static class TratamientoEndpoints
{
    public static void MapTratamientoEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/tratamientos")
                       .WithTags("Tratamientos");

        // GET /api/tratamientos
        group.MapGet("/", async (IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ListarTratamientosQuery(), ct);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error);
        });

        // GET /api/tratamientos/{id}
        group.MapGet("/{id:long}", async (long id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ObtenerTratamientoPorIdQuery(id), ct);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(result.Error);
        });

        // POST /api/tratamientos
        group.MapPost("/", async (CrearTratamientoRequest request, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new CrearTratamientoCommand(
        request.Nombre,
        request.HistoriaClinica,
        request.Estado,
        request.VentaLibre,
        request.IdCategoria
    ), ct);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Conflict(result.Error);
        });

        // PUT /api/tratamientos/{id}
        group.MapPut("/{id:long}", async (long id, ActualizarTratamientoRequest request, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ActualizarTratamientoCommand(
        id,
        request.Nombre,
        request.HistoriaClinica,
        request.Estado,
        request.VentaLibre,
        request.IdCategoria
    ), ct);
            return result.IsSuccess
                ? Results.Ok()
                : Results.Conflict(result.Error);
        });

        // DELETE /api/tratamientos/{id}
        group.MapDelete("/{id:long}", async (long id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new EliminarTratamientoCommand(id), ct);
            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(result.Error);
        });
    }
}
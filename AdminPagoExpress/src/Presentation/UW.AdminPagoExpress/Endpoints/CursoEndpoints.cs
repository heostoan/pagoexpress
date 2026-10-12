using UW.AdminPagoExpress.Application.Features.Cursos.Commands.ActualizarCurso;
using UW.AdminPagoExpress.Application.Features.Cursos.Commands.CrearCurso;
using UW.AdminPagoExpress.Application.Features.Cursos.Commands.EliminarCurso;
using UW.AdminPagoExpress.Application.Features.Cursos.Queries.ListarCursos;
using UW.AdminPagoExpress.Application.Features.Cursos.Queries.ObtenerCursoPorId;
using UW.AdminPagoExpress.Shared.Request;

namespace UW.AdminPagoExpress.Endpoints;

public static class CursoEndpoints
{
    public static void MapCursoEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/cursos").WithTags("Cursos");

        group.MapGet("/", async (IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ListarCursosQuery(), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });

        group.MapGet("/{id:long}", async (long id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ObtenerCursoPorIdQuery(id), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
        });

        group.MapPost("/", async (CrearCursoRequest request, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new CrearCursoCommand(
                request.Nombre,
                request.CodigoCurso,
                request.Ciclo,
                request.Sis,
                request.Estado
            ), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.Conflict(result.Error);
        });

        group.MapPut("/{id:long}", async (long id, ActualizarCursoRequest request, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ActualizarCursoCommand(
                id,
                request.Nombre,
                request.CodigoCurso,
                request.Ciclo,
                request.Sis,
                request.Estado
            ), ct);
            return result.IsSuccess ? Results.Ok() : Results.Conflict(result.Error);
        });

        group.MapDelete("/{id:long}", async (long id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new EliminarCursoCommand(id), ct);
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });
    }
}

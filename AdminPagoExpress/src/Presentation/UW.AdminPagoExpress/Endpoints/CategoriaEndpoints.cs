using UW.AdminPagoExpress.Application.Features.Categorias.Commands.ActualizarCategoria;
using UW.AdminPagoExpress.Application.Features.Categorias.Commands.CrearCategoria;
using UW.AdminPagoExpress.Application.Features.Categorias.Commands.EliminarCategoria;
using UW.AdminPagoExpress.Application.Features.Categorias.Queries.ListarCategorias;
using UW.AdminPagoExpress.Application.Features.Categorias.Queries.ObtenerCategoriaPorId;
using UW.AdminPagoExpress.Shared.Request;

namespace UW.AdminPagoExpress.Endpoints;

public static class CategoriaEndpoints
{
    public static void MapCategoriaEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/categorias").WithTags("Categorias");

        group.MapGet("/", async (IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ListarCategoriasQuery(), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });

        group.MapGet("/{id:long}", async (long id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ObtenerCategoriaPorIdQuery(id), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
        });

        group.MapPost("/", async (CrearCategoriaRequest request, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new CrearCategoriaCommand(request.Nombre, request.Estado, request.Descripcion), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.Conflict(result.Error);
        });

        group.MapPut("/{id:long}", async (long id, ActualizarCategoriaRequest request, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ActualizarCategoriaCommand(id, request.Nombre, request.Estado, request.Descripcion), ct);
            return result.IsSuccess ? Results.Ok() : Results.Conflict(result.Error);
        });

        group.MapDelete("/{id:long}", async (long id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new EliminarCategoriaCommand(id), ct);
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });
    }
}

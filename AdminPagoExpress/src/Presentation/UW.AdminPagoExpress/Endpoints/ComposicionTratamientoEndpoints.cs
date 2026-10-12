using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Domain.Entities;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Endpoints;

public static class ComposicionTratamientoEndpoints
{
    public static void MapComposicionTratamientoEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/composicion-tratamiento").WithTags("ComposicionTratamiento");

        // Listar tratamientos (para seleccionar relacionados)
        group.MapGet("/tratamientos", async (ITratamientoRepository repo, CancellationToken ct) =>
        {
            var list = await repo.ListarAsync(ct);
            var resp = list.Select(t => new TratamientoResponse(t.Id, t.Nombre, t.HistoriaClinica, t.Estado, t.VentaLibre, t.IdCategoria)).ToList();
            return Results.Ok(resp);
        });

        // Listar composiciones por tratamiento padre
        group.MapGet("/por-tratamiento/{id:long}", async (long id, IComposicionTratamientoRepository repo, CancellationToken ct) =>
        {
            var list = await repo.ListarPorTratamientoAsync(id, ct);
            var resp = list.Select(c => new ComposicionTratamientoResponse(c.IdComposicionTratamiento, c.IdTratamiento, c.CodigoOracle, c.Cantidad, c.UnidadMedida)).ToList();
            return Results.Ok(resp);
        });

        // Listar todas las composiciones (para precarga en cliente)
        group.MapGet("/todas", async (IComposicionTratamientoRepository repo, CancellationToken ct) =>
        {
            var list = await repo.ListarTodasAsync(ct);
            var resp = list.Select(c => new ComposicionTratamientoResponse(c.IdComposicionTratamiento, c.IdTratamiento, c.CodigoOracle, c.Cantidad, c.UnidadMedida)).ToList();
            return Results.Ok(resp);
        });

        // Crear varias composiciones
        group.MapPost("/", async (CrearComposicionTratamientoRequest request, IComposicionTratamientoRepository repo, CancellationToken ct) =>
        {
            if (request.Items == null || !request.Items.Any()) return Results.BadRequest("No hay items para crear.");

            var entidades = request.Items.Select(i => ComposicionTratamiento.Crear(request.IdTratamiento, i.CodigoOracle ?? string.Empty, i.Cantidad, i.UnidadMedida ?? string.Empty)).ToList();
            await repo.CrearVariosAsync(entidades, ct);
            return Results.Ok();
        });

        // Actualizar estado (single)
        group.MapPut("/estado", async (ActualizarEstadoComposicionTratamientoRequest request, IComposicionTratamientoRepository repo, CancellationToken ct) =>
        {
            await repo.ActualizarEstadoAsync(request.IdComposicion, ct);
            return Results.Ok();
        });

        // Bulk update estados
        group.MapPut("/estados", async (List<ActualizarEstadoComposicionTratamientoRequest> requests, IComposicionTratamientoRepository repo, CancellationToken ct) =>
        {
            foreach (var r in requests)
            {
                await repo.ActualizarEstadoAsync(r.IdComposicion, ct);
            }
            return Results.Ok();
        });
    }
}

using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Domain.Entities;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Endpoints;

public static class DetalleTratamientoCursoEndpoints
{
    public static void MapDetalleTratamientoCursoEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/detalle-tratamiento-curso").WithTags("DetalleTratamientoCurso");

        group.MapGet("/tratamientos", async (ITratamientoRepository repo, CancellationToken ct) =>
        {
            var list = await repo.ListarAsync(ct);
            var resp = list.Select(t => new TratamientoResponse(t.Id, t.Nombre, t.HistoriaClinica, t.Estado, t.VentaLibre, t.IdCategoria)).ToList();
            return Results.Ok(resp);
        });

        group.MapGet("/por-curso/{id:long}", async (long id, IDetalleTratamientoCursoRepository repo, CancellationToken ct) =>
        {
            var detalles = await repo.ListarPorCursoAsync(id, ct);
            var resp = detalles.Select(d => new DetalleTratamientoCursoResponse(d.IdDetalleTratamientoCurso, d.IdTratamiento, d.IdCurso, d.Estado)).ToList();
            return Results.Ok(resp);
        });

        group.MapPost("/", async (CrearDetalleTratamientoCursoRequest request, IDetalleTratamientoCursoRepository repo, CancellationToken ct) =>
        {
            if (request.IdTratamientos == null || !request.IdTratamientos.Any())
                return Results.BadRequest("No se enviaron tratamientos.");

            var entidades = request.IdTratamientos.Select(idT => DetalleTratamientoCurso.Crear(idT, request.IdCurso)).ToList();
            await repo.CrearVariosAsync(entidades, ct);
            return Results.Ok();
        });

        group.MapPut("/estado", async (ActualizarEstadoDetalleTratamientoCursoRequest request, IDetalleTratamientoCursoRepository repo, CancellationToken ct) =>
        {
            await repo.ActualizarEstadoAsync(request.IdDetalle, request.Estado, ct);
            return Results.Ok();
        });
    }
}

using UW.AdminPagoExpress.Application.Features.Pacientes.Commands.CrearPaciente;
using UW.AdminPagoExpress.Application.Features.Pacientes.Commands.ActualizarPaciente;
using UW.AdminPagoExpress.Application.Features.Pacientes.Commands.EliminarPaciente;
using UW.AdminPagoExpress.Application.Features.Pacientes.Queries.ListarPacientes;
using UW.AdminPagoExpress.Application.Features.Pacientes.Queries.ObtenerPacientePorId;
using UW.AdminPagoExpress.Shared.Request;

namespace UW.AdminPagoExpress.Endpoints;

public static class PacienteEndpoints
{
    public static void MapPacienteEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/pacientes").WithTags("Pacientes");

        group.MapGet("/", async (IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ListarPacientesQuery(), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        });

        group.MapGet("/{id:long}", async (long id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ObtenerPacientePorIdQuery(id), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
        });

        group.MapPost("/", async (CrearPacienteRequest request, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new CrearPacienteCommand(
                request.TipoDocumento,
                request.DocIdentidad,
                request.Nombres,
                request.ApellidoPaterno,
                request.ApellidoMaterno,
                request.Genero,
                request.Celular,
                request.Correo,
                request.FechaNacimiento ?? DateTime.UtcNow,
                request.IdAlumno,
                request.IdFirma
            ), ct);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.Conflict(result.Error);
        });

        group.MapPut("/{id:long}", async (long id, ActualizarPacienteRequest request, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new ActualizarPacienteCommand(
                id,
                request.TipoDocumento,
                request.DocIdentidad,
                request.Nombres,
                request.ApellidoPaterno,
                request.ApellidoMaterno,
                request.Genero,
                request.Celular,
                request.Correo,
                request.FechaNacimiento ?? DateTime.UtcNow,
                request.IdAlumno,
                request.IdFirma
            ), ct);

            return result.IsSuccess ? Results.Ok() : Results.Conflict(result.Error);
        });

        group.MapDelete("/{id:long}", async (long id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new EliminarPacienteCommand(id), ct);
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });
    }
}

using MediatR;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Application.Features.Pacientes.Queries.ListarPacientes;

public record ListarPacientesQuery() : IRequest<Result<List<PacienteResponse>>>;

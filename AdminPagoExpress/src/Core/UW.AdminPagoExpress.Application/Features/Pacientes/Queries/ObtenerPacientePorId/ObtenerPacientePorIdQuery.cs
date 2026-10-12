using MediatR;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Application.Features.Pacientes.Queries.ObtenerPacientePorId;

public record ObtenerPacientePorIdQuery(long Id) : IRequest<Result<PacienteResponse>>;

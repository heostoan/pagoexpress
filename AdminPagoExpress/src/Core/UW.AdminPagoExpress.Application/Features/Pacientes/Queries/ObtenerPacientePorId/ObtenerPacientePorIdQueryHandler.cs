using MediatR;
using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Application.Features.Pacientes.Queries.ObtenerPacientePorId;

public class ObtenerPacientePorIdQueryHandler : IRequestHandler<ObtenerPacientePorIdQuery, Result<PacienteResponse>>
{
    private readonly IPacienteRepository _repo;
    public ObtenerPacientePorIdQueryHandler(IPacienteRepository repo) => _repo = repo;

    public async Task<Result<PacienteResponse>> Handle(ObtenerPacientePorIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repo.ObtenerPorIdAsync(request.Id, cancellationToken);
        if (item == null)
            return Result<PacienteResponse>.Create<PacienteResponse>(null);
        return Result<PacienteResponse>.Success(item);
    }
}

using MediatR;
using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Application.Features.Pacientes.Queries.ListarPacientes;

public class ListarPacientesQueryHandler : IRequestHandler<ListarPacientesQuery, Result<List<PacienteResponse>>>
{
    private readonly IPacienteRepository _repo;
    public ListarPacientesQueryHandler(IPacienteRepository repo) => _repo = repo;

    public async Task<Result<List<PacienteResponse>>> Handle(ListarPacientesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repo.ListarAsync(cancellationToken);
        return Result<List<PacienteResponse>>.Success(items);
    }
}

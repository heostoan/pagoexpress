using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Shared.Response;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Tarifarios.Queries.ListarTarifarios;

public record ListarTarifariosQuery() : IRequest<Result<List<TarifarioResponse>>>;

public class ListarTarifariosQueryHandler : IRequestHandler<ListarTarifariosQuery, Result<List<TarifarioResponse>>>
{
    private readonly ITarifarioRepository _tarifarioRepository;

    public ListarTarifariosQueryHandler(ITarifarioRepository tarifarioRepository)
    {
        _tarifarioRepository = tarifarioRepository;
    }

    public async Task<Result<List<TarifarioResponse>>> Handle(ListarTarifariosQuery request, CancellationToken cancellationToken)
    {
        var items = await _tarifarioRepository.ListarAsync(cancellationToken);
        var response = items.Select(t => new TarifarioResponse(t.Id, t.PrecioAlumno, t.PrecioExterno, t.PrecioEmpleado, t.CodigoSpring, t.CodigoOracle, t.IdTratamiento)).ToList();
        return Result.Success(response);
    }
}

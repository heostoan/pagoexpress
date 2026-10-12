using UW.AdminPagoExpress.Domain.Errors;
using UW.AdminPagoExpress.Shared.Response;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Tarifarios.Queries.ObtenerTarifarioPorId;

public record ObtenerTarifarioPorIdQuery(long Id) : IRequest<Result<TarifarioResponse>>;

public class ObtenerTarifarioPorIdQueryHandler : IRequestHandler<ObtenerTarifarioPorIdQuery, Result<TarifarioResponse>>
{
    private readonly ITarifarioRepository _tarifarioRepository;

    public ObtenerTarifarioPorIdQueryHandler(ITarifarioRepository tarifarioRepository)
    {
        _tarifarioRepository = tarifarioRepository;
    }

    public async Task<Result<TarifarioResponse>> Handle(ObtenerTarifarioPorIdQuery request, CancellationToken cancellationToken)
    {
        var t = await _tarifarioRepository.ObtenerPorIdAsync(request.Id, cancellationToken);
        if (t is null) return Result.Failure<TarifarioResponse>(TarifarioErrors.NoEncontrado);
        var resp = new TarifarioResponse(t.Id, t.PrecioAlumno, t.PrecioExterno, t.PrecioEmpleado, t.CodigoSpring, t.CodigoOracle, t.IdTratamiento);
        return Result.Success(resp);
    }
}

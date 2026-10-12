using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Shared.Response;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Tratamientos.Queries.ListarTratamientos;

public record ListarTratamientosQuery() : IRequest<Result<List<TratamientoResponse>>>;

public class ListarTratamientosQueryHandler
    : IRequestHandler<ListarTratamientosQuery, Result<List<TratamientoResponse>>>
{
    private readonly ITratamientoRepository _tratamientoRepository;

    public ListarTratamientosQueryHandler(ITratamientoRepository tratamientoRepository)
    {
        _tratamientoRepository = tratamientoRepository;
    }

    public async Task<Result<List<TratamientoResponse>>> Handle(
        ListarTratamientosQuery request, CancellationToken cancellationToken)
    {
        var tratamientos = await _tratamientoRepository.ListarAsync(cancellationToken);

        var response = tratamientos
            .Select(t => new TratamientoResponse(
                t.Id, t.Nombre, t.HistoriaClinica, t.Estado, t.VentaLibre, t.IdCategoria))
            .ToList();

        return Result.Success(response);
    }
}
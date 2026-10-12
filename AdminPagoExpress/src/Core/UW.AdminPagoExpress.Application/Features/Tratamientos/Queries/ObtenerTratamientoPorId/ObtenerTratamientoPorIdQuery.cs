using UW.AdminPagoExpress.Domain.Errors;
using UW.AdminPagoExpress.Shared.Response;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Tratamientos.Queries.ObtenerTratamientoPorId;

public record ObtenerTratamientoPorIdQuery(long Id) : IRequest<Result<TratamientoResponse>>;

public class ObtenerTratamientoPorIdQueryHandler
    : IRequestHandler<ObtenerTratamientoPorIdQuery, Result<TratamientoResponse>>
{
    private readonly ITratamientoRepository _tratamientoRepository;

    public ObtenerTratamientoPorIdQueryHandler(ITratamientoRepository tratamientoRepository)
    {
        _tratamientoRepository = tratamientoRepository;
    }

    public async Task<Result<TratamientoResponse>> Handle(
        ObtenerTratamientoPorIdQuery request, CancellationToken cancellationToken)
    {
        var tratamiento = await _tratamientoRepository.ObtenerPorIdAsync(request.Id, cancellationToken);
        if (tratamiento is null)
            return Result.Failure<TratamientoResponse>(TratamientoErrors.NoEncontrado);

        var response = new TratamientoResponse(
            tratamiento.Id, tratamiento.Nombre, tratamiento.HistoriaClinica,
            tratamiento.Estado, tratamiento.VentaLibre, tratamiento.IdCategoria);

        return Result.Success(response);
    }
}
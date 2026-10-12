using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Domain.Errors;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Tratamientos.Commands.EliminarTratamiento;

public record EliminarTratamientoCommand(long Id) : IRequest<Result>;

public class EliminarTratamientoCommandHandler : IRequestHandler<EliminarTratamientoCommand, Result>
{
    private readonly ITratamientoRepository _tratamientoRepository;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public EliminarTratamientoCommandHandler(
        ITratamientoRepository tratamientoRepository, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _tratamientoRepository = tratamientoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(EliminarTratamientoCommand command, CancellationToken cancellationToken)
    {
        var tratamiento = await _tratamientoRepository.ObtenerPorIdAsync(command.Id, cancellationToken);
        if (tratamiento is null)
            return Result.Failure(TratamientoErrors.NoEncontrado);

        _tratamientoRepository.Eliminar(tratamiento);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Result.Success();
    }
}
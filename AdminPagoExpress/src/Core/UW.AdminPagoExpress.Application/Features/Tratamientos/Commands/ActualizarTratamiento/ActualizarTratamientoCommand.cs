using UW.AdminPagoExpress.Domain.Errors;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Tratamientos.Commands.ActualizarTratamiento;

public record ActualizarTratamientoCommand(
    long Id,
    string Nombre,
    string HistoriaClinica,
    string Estado,
    string VentaLibre,
    long IdCategoria
) : IRequest<Result>;

public class ActualizarTratamientoCommandHandler : IRequestHandler<ActualizarTratamientoCommand, Result>
{
    private readonly ITratamientoRepository _tratamientoRepository;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public ActualizarTratamientoCommandHandler(
        ITratamientoRepository tratamientoRepository, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _tratamientoRepository = tratamientoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ActualizarTratamientoCommand command, CancellationToken cancellationToken)
    {
        var tratamiento = await _tratamientoRepository.ObtenerPorIdAsync(command.Id, cancellationToken);
        if (tratamiento is null)
            return Result.Failure(TratamientoErrors.NoEncontrado);

        bool existeNombre = await _tratamientoRepository
            .ExisteNombreAsync(command.Nombre, command.Id, cancellationToken);
        if (existeNombre)
            return Result.Failure(TratamientoErrors.NombreDuplicado);

        tratamiento.Actualizar(
            command.Nombre, command.HistoriaClinica, command.Estado,
            command.VentaLibre, command.IdCategoria);

        _tratamientoRepository.Actualizar(tratamiento);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Result.Success();
    }
}

public class ActualizarTratamientoCommandValidator : AbstractValidator<ActualizarTratamientoCommand>
{
    public ActualizarTratamientoCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID es inválido.");
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(200);
        RuleFor(x => x.IdCategoria).GreaterThan(0);
    }
}
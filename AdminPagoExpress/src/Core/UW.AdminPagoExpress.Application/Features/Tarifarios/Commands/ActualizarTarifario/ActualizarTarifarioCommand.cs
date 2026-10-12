using UW.AdminPagoExpress.Domain.Errors;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Tarifarios.Commands.ActualizarTarifario;

public record ActualizarTarifarioCommand(
    long Id,
    decimal PrecioAlumno,
    decimal PrecioExterno,
    decimal PrecioEmpleado,
    string CodigoSpring,
    string CodigoOracle,
    long IdTratamiento
) : IRequest<Result>;

public class ActualizarTarifarioCommandHandler : IRequestHandler<ActualizarTarifarioCommand, Result>
{
    private readonly ITarifarioRepository _tarifarioRepository;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public ActualizarTarifarioCommandHandler(ITarifarioRepository tarifarioRepository, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _tarifarioRepository = tarifarioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ActualizarTarifarioCommand command, CancellationToken cancellationToken)
    {
        var tarifario = await _tarifarioRepository.ObtenerPorIdAsync(command.Id, cancellationToken);
        if (tarifario is null)
            return Result.Failure(TarifarioErrors.NoEncontrado);

        bool existe = await _tarifarioRepository.ExisteCodigoAsync(command.CodigoSpring, command.CodigoOracle, command.Id, cancellationToken);
        if (existe)
            return Result.Failure(TarifarioErrors.CodigoDuplicado);

        tarifario.Actualizar(command.PrecioAlumno, command.PrecioExterno, command.PrecioEmpleado, command.CodigoSpring, command.CodigoOracle, command.IdTratamiento);

        _tarifarioRepository.Actualizar(tarifario);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Result.Success();
    }
}

public class ActualizarTarifarioCommandValidator : AbstractValidator<ActualizarTarifarioCommand>
{
    public ActualizarTarifarioCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.PrecioAlumno).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PrecioExterno).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PrecioEmpleado).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CodigoSpring).NotEmpty().MaximumLength(50);
        RuleFor(x => x.CodigoOracle).NotEmpty().MaximumLength(50);
        RuleFor(x => x.IdTratamiento).GreaterThan(0);
    }
}

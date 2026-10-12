using UW.AdminPagoExpress.Domain.Entities;
using UW.AdminPagoExpress.Domain.Errors;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Tarifarios.Commands.CrearTarifario;

public record CrearTarifarioCommand(
    decimal PrecioAlumno,
    decimal PrecioExterno,
    decimal PrecioEmpleado,
    string CodigoSpring,
    string CodigoOracle,
    long IdTratamiento
) : IRequest<Result<long>>;

public class CrearTarifarioCommandHandler : IRequestHandler<CrearTarifarioCommand, Result<long>>
{
    private readonly ITarifarioRepository _tarifarioRepository;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public CrearTarifarioCommandHandler(ITarifarioRepository tarifarioRepository, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _tarifarioRepository = tarifarioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<long>> Handle(CrearTarifarioCommand command, CancellationToken cancellationToken)
    {
        bool existe = await _tarifarioRepository.ExisteCodigoAsync(command.CodigoSpring, command.CodigoOracle, null, cancellationToken);
        if (existe)
            return Result.Failure<long>(TarifarioErrors.CodigoDuplicado);

        var tarifario = Tarifario.Crear(
            command.PrecioAlumno,
            command.PrecioExterno,
            command.PrecioEmpleado,
            command.CodigoSpring,
            command.CodigoOracle,
            command.IdTratamiento);

        _tarifarioRepository.Agregar(tarifario);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Result.Success(tarifario.Id);
    }
}

public class CrearTarifarioCommandValidator : AbstractValidator<CrearTarifarioCommand>
{
    public CrearTarifarioCommandValidator()
    {
        RuleFor(x => x.PrecioAlumno).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PrecioExterno).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PrecioEmpleado).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CodigoSpring).NotEmpty().MaximumLength(50);
        RuleFor(x => x.CodigoOracle).NotEmpty().MaximumLength(50);
        RuleFor(x => x.IdTratamiento).GreaterThan(0);
    }
}

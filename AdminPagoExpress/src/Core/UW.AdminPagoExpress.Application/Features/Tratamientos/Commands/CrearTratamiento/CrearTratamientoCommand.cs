using UW.AdminPagoExpress.Domain.Entities;
using UW.AdminPagoExpress.Domain.Errors;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Tratamientos.Commands.CrearTratamiento;

public record CrearTratamientoCommand(
    string Nombre,
    string HistoriaClinica,
    string Estado,
    string VentaLibre,
    long IdCategoria
) : IRequest<Result<long>>;

public class CrearTratamientoCommandHandler : IRequestHandler<CrearTratamientoCommand, Result<long>>
{
    private readonly ITratamientoRepository _tratamientoRepository;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public CrearTratamientoCommandHandler(ITratamientoRepository tratamientoRepository, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _tratamientoRepository = tratamientoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<long>> Handle(CrearTratamientoCommand command, CancellationToken cancellationToken)
    {
        bool existeNombre = await _tratamientoRepository.ExisteNombreAsync(command.Nombre, null, cancellationToken);
        if (existeNombre)
            return Result.Failure<long>(TratamientoErrors.NombreDuplicado);

        var tratamiento = Tratamiento.Crear(
            command.Nombre,
            command.HistoriaClinica,
            command.Estado,
            command.VentaLibre,
            command.IdCategoria
        );

        _tratamientoRepository.Agregar(tratamiento);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Result.Success(tratamiento.Id);
    }
}

public class CrearTratamientoCommandValidator : AbstractValidator<CrearTratamientoCommand>
{
    public CrearTratamientoCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(200).WithMessage("El nombre no debe exceder los 200 caracteres.");

        RuleFor(x => x.IdCategoria)
            .GreaterThan(0).WithMessage("Debe seleccionar una categoría válida.");
    }
}
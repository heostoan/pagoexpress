using UW.AdminPagoExpress.Domain.Entities;
using UW.AdminPagoExpress.Domain.Errors;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Cursos.Commands.CrearCurso;

public record CrearCursoCommand(
    string Nombre,
    string CodigoCurso,
    string Ciclo,
    string Sis,
    string Estado
) : IRequest<Result<long>>;

public class CrearCursoCommandHandler : IRequestHandler<CrearCursoCommand, Result<long>>
{
    private readonly ICursoRepository _cursoRepository;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public CrearCursoCommandHandler(ICursoRepository cursoRepository, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<long>> Handle(CrearCursoCommand command, CancellationToken cancellationToken)
    {
        bool existeNombre = await _cursoRepository.ExisteNombreAsync(command.Nombre, null, cancellationToken);
        if (existeNombre)
            return Result.Failure<long>(CursoErrors.NombreDuplicado);

        var curso = Curso.Crear(
            command.Nombre,
            command.CodigoCurso,
            command.Ciclo,
            command.Sis,
            command.Estado
        );

        _cursoRepository.Agregar(curso);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Result.Success(curso.Id);
    }
}

public class CrearCursoCommandValidator : AbstractValidator<CrearCursoCommand>
{
    public CrearCursoCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(200).WithMessage("El nombre no debe exceder los 200 caracteres.");
    }
}

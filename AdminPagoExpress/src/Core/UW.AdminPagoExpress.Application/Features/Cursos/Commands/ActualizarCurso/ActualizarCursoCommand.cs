using UW.AdminPagoExpress.Domain.Errors;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Cursos.Commands.ActualizarCurso;

public record ActualizarCursoCommand(
    long Id,
    string Nombre,
    string CodigoCurso,
    string Ciclo,
    string Sis,
    string Estado
) : IRequest<Result>;

public class ActualizarCursoCommandHandler : IRequestHandler<ActualizarCursoCommand, Result>
{
    private readonly ICursoRepository _cursoRepository;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public ActualizarCursoCommandHandler(ICursoRepository cursoRepository, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ActualizarCursoCommand command, CancellationToken cancellationToken)
    {
        var curso = await _cursoRepository.ObtenerPorIdAsync(command.Id, cancellationToken);
        if (curso is null)
            return Result.Failure(CursoErrors.NoEncontrado);

        bool existeNombre = await _cursoRepository.ExisteNombreAsync(command.Nombre, command.Id, cancellationToken);
        if (existeNombre)
            return Result.Failure(CursoErrors.NombreDuplicado);

        curso.Actualizar(command.Nombre, command.CodigoCurso, command.Ciclo, command.Sis, command.Estado);

        _cursoRepository.Actualizar(curso);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Result.Success();
    }
}

public class ActualizarCursoCommandValidator : AbstractValidator<ActualizarCursoCommand>
{
    public ActualizarCursoCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID es inválido.");
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(200);
    }
}

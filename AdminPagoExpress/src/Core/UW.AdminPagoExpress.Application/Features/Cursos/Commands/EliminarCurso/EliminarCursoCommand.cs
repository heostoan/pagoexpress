using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Domain.Errors;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Cursos.Commands.EliminarCurso;

public record EliminarCursoCommand(long Id) : IRequest<Result>;

public class EliminarCursoCommandHandler : IRequestHandler<EliminarCursoCommand, Result>
{
    private readonly ICursoRepository _cursoRepository;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public EliminarCursoCommandHandler(ICursoRepository cursoRepository, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(EliminarCursoCommand command, CancellationToken cancellationToken)
    {
        var curso = await _cursoRepository.ObtenerPorIdAsync(command.Id, cancellationToken);
        if (curso is null)
            return Result.Failure(CursoErrors.NoEncontrado);

        _cursoRepository.Eliminar(curso);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Result.Success();
    }
}

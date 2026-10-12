using UW.AdminPagoExpress.Domain.Errors;
using UW.AdminPagoExpress.Shared.Response;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Cursos.Queries.ObtenerCursoPorId;

public record ObtenerCursoPorIdQuery(long Id) : IRequest<Result<CursoResponse>>;

public class ObtenerCursoPorIdQueryHandler : IRequestHandler<ObtenerCursoPorIdQuery, Result<CursoResponse>>
{
    private readonly ICursoRepository _cursoRepository;

    public ObtenerCursoPorIdQueryHandler(ICursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }

    public async Task<Result<CursoResponse>> Handle(ObtenerCursoPorIdQuery request, CancellationToken cancellationToken)
    {
        var curso = await _cursoRepository.ObtenerPorIdAsync(request.Id, cancellationToken);
        if (curso is null)
            return Result.Failure<CursoResponse>(CursoErrors.NoEncontrado);

        var response = new CursoResponse(curso.Id, curso.Nombre, curso.CodigoCurso, curso.Ciclo, curso.Sis, curso.Estado);
        return Result.Success(response);
    }
}

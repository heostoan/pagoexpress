using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Shared.Response;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Cursos.Queries.ListarCursos;

public record ListarCursosQuery() : IRequest<Result<List<CursoResponse>>>;

public class ListarCursosQueryHandler : IRequestHandler<ListarCursosQuery, Result<List<CursoResponse>>>
{
    private readonly ICursoRepository _cursoRepository;

    public ListarCursosQueryHandler(ICursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }

    public async Task<Result<List<CursoResponse>>> Handle(ListarCursosQuery request, CancellationToken cancellationToken)
    {
        var cursos = await _cursoRepository.ListarAsync(cancellationToken);

        var response = cursos
            .Select(c => new CursoResponse(c.Id, c.Nombre, c.CodigoCurso, c.Ciclo, c.Sis, c.Estado))
            .ToList();

        return Result.Success(response);
    }
}

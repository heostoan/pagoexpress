using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Shared.Response;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Categorias.Queries.ListarCategorias;

public record ListarCategoriasQuery() : IRequest<Result<List<CategoriaResponse>>>;

public class ListarCategoriasQueryHandler : IRequestHandler<ListarCategoriasQuery, Result<List<CategoriaResponse>>>
{
    private readonly ICategoriaRepository _categoriaRepository;

    public ListarCategoriasQueryHandler(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<Result<List<CategoriaResponse>>> Handle(ListarCategoriasQuery request, CancellationToken cancellationToken)
    {
        var categorias = await _categoriaRepository.ListarAsync(cancellationToken);

        var response = categorias
            .Select(c => new CategoriaResponse(c.Id, c.Nombre, c.Estado, c.Descripcion))
            .ToList();

        return Result.Success(response);
    }
}

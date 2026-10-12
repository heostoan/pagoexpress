using UW.AdminPagoExpress.Domain.Errors;
using UW.AdminPagoExpress.Shared.Response;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Categorias.Queries.ObtenerCategoriaPorId;

public record ObtenerCategoriaPorIdQuery(long Id) : IRequest<Result<CategoriaResponse>>;

public class ObtenerCategoriaPorIdQueryHandler : IRequestHandler<ObtenerCategoriaPorIdQuery, Result<CategoriaResponse>>
{
    private readonly ICategoriaRepository _categoriaRepository;

    public ObtenerCategoriaPorIdQueryHandler(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<Result<CategoriaResponse>> Handle(ObtenerCategoriaPorIdQuery request, CancellationToken cancellationToken)
    {
        var categoria = await _categoriaRepository.ObtenerPorIdAsync(request.Id, cancellationToken);
        if (categoria is null)
            return Result.Failure<CategoriaResponse>(CategoriaErrors.NoEncontrada);

        var response = new CategoriaResponse(categoria.Id, categoria.Nombre, categoria.Estado, categoria.Descripcion);
        return Result.Success(response);
    }
}

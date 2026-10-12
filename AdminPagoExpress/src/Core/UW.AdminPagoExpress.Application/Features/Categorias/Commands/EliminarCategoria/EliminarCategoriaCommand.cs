using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Domain.Errors;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Categorias.Commands.EliminarCategoria;

public record EliminarCategoriaCommand(long Id) : IRequest<Result>;

public class EliminarCategoriaCommandHandler : IRequestHandler<EliminarCategoriaCommand, Result>
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public EliminarCategoriaCommandHandler(ICategoriaRepository categoriaRepository, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _categoriaRepository = categoriaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(EliminarCategoriaCommand command, CancellationToken cancellationToken)
    {
        var categoria = await _categoriaRepository.ObtenerPorIdAsync(command.Id, cancellationToken);
        if (categoria is null)
            return Result.Failure(CategoriaErrors.NoEncontrada);

        _categoriaRepository.Eliminar(categoria);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Result.Success();
    }
}

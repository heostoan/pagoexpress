using UW.AdminPagoExpress.Domain.Errors;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Categorias.Commands.ActualizarCategoria;

public record ActualizarCategoriaCommand(
    long Id,
    string Nombre,
    string Estado,
    string? Descripcion
) : IRequest<Result>;

public class ActualizarCategoriaCommandHandler : IRequestHandler<ActualizarCategoriaCommand, Result>
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public ActualizarCategoriaCommandHandler(ICategoriaRepository categoriaRepository, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _categoriaRepository = categoriaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ActualizarCategoriaCommand command, CancellationToken cancellationToken)
    {
        var categoria = await _categoriaRepository.ObtenerPorIdAsync(command.Id, cancellationToken);
        if (categoria is null)
            return Result.Failure(CategoriaErrors.NoEncontrada);

        bool existeNombre = await _categoriaRepository.ExisteNombreAsync(command.Nombre, command.Id, cancellationToken);
        if (existeNombre)
            return Result.Failure(CategoriaErrors.NombreDuplicado);

        categoria.Actualizar(command.Nombre, command.Estado, command.Descripcion);

        _categoriaRepository.Actualizar(categoria);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Result.Success();
    }
}

public class ActualizarCategoriaCommandValidator : AbstractValidator<ActualizarCategoriaCommand>
{
    public ActualizarCategoriaCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID es inválido.");
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(50);
    }
}

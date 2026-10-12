using UW.AdminPagoExpress.Domain.Entities;
using UW.AdminPagoExpress.Domain.Errors;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Categorias.Commands.CrearCategoria;

public record CrearCategoriaCommand(
    string Nombre,
    string Estado,
    string? Descripcion
) : IRequest<Result<long>>;

public class CrearCategoriaCommandHandler : IRequestHandler<CrearCategoriaCommand, Result<long>>
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public CrearCategoriaCommandHandler(ICategoriaRepository categoriaRepository, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _categoriaRepository = categoriaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<long>> Handle(CrearCategoriaCommand command, CancellationToken cancellationToken)
    {
        bool existeNombre = await _categoriaRepository.ExisteNombreAsync(command.Nombre, null, cancellationToken);
        if (existeNombre)
            return Result.Failure<long>(CategoriaErrors.NombreDuplicado);

        var categoria = Categoria.Crear(command.Nombre, command.Estado, command.Descripcion);

        _categoriaRepository.Agregar(categoria);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Result.Success(categoria.Id);
    }
}

public class CrearCategoriaCommandValidator : AbstractValidator<CrearCategoriaCommand>
{
    public CrearCategoriaCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre no debe exceder los 50 caracteres.");
    }
}

using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Domain.Errors;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Tarifarios.Commands.EliminarTarifario;

public record EliminarTarifarioCommand(long Id) : IRequest<Result>;

public class EliminarTarifarioCommandHandler : IRequestHandler<EliminarTarifarioCommand, Result>
{
    private readonly ITarifarioRepository _tarifarioRepository;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public EliminarTarifarioCommandHandler(ITarifarioRepository tarifarioRepository, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _tarifarioRepository = tarifarioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(EliminarTarifarioCommand command, CancellationToken cancellationToken)
    {
        var tarifario = await _tarifarioRepository.ObtenerPorIdAsync(command.Id, cancellationToken);
        if (tarifario is null)
            return Result.Failure(TarifarioErrors.NoEncontrado);

        _tarifarioRepository.Eliminar(tarifario);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Result.Success();
    }
}

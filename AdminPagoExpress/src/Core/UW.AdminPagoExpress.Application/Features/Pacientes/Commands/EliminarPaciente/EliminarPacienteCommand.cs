using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Pacientes.Commands.EliminarPaciente;

public record EliminarPacienteCommand(long Id) : IRequest<Result<bool>>;

public class EliminarPacienteCommandHandler : IRequestHandler<EliminarPacienteCommand, Result<bool>>
{
    private readonly IPacienteRepository _repo;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public EliminarPacienteCommandHandler(IPacienteRepository repo, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(EliminarPacienteCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repo.ObtenerPorIdAsync(request.Id, cancellationToken);
        if (existing == null) return Result.Failure<bool>(UW.AdminPagoExpress.Domain.Common.Error.NullValue);

        var deleted = await _repo.EliminarAsync(request.Id, cancellationToken);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);
        return Result.Success(deleted);
    }
}

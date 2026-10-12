using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Pacientes.Commands.ActualizarPaciente;

public record ActualizarPacienteCommand(
    long Id,
    string TipoDocumento,
    string DocIdentidad,
    string Nombres,
    string ApellidoPaterno,
    string ApellidoMaterno,
    string Genero,
    string Celular,
    string Correo,
    DateTime FechaNacimiento,
    long IdAlumno,
    long IdFirma
) : IRequest<Result<bool>>;

public class ActualizarPacienteCommandHandler : IRequestHandler<ActualizarPacienteCommand, Result<bool>>
{
    private readonly IPacienteRepository _repo;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public ActualizarPacienteCommandHandler(IPacienteRepository repo, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(ActualizarPacienteCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repo.ObtenerPorIdAsync(request.Id, cancellationToken);
        if (existing == null) return Result.Failure<bool>(UW.AdminPagoExpress.Domain.Common.Error.NullValue);

        var updated = await _repo.ActualizarAsync(request.Id, new UW.AdminPagoExpress.Shared.Request.ActualizarPacienteRequest
        {
            Nombres = request.Nombres,
            ApellidoPaterno = request.ApellidoPaterno,
            ApellidoMaterno = request.ApellidoMaterno,
            DocIdentidad = request.DocIdentidad,
            FechaNacimiento = request.FechaNacimiento,
            Estado = "1",
        }, cancellationToken);

        await _unitOfWork.GuardarCambiosAsync(cancellationToken);
        return Result.Success(updated);
    }
}

public class ActualizarPacienteCommandValidator : AbstractValidator<ActualizarPacienteCommand>
{
    public ActualizarPacienteCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Nombres).NotEmpty();
    }
}

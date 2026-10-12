using UW.AdminPagoExpress.Domain.Entities;
using Result = UW.AdminPagoExpress.Application.Common.Result;

namespace UW.AdminPagoExpress.Application.Features.Pacientes.Commands.CrearPaciente;

public record CrearPacienteCommand(
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
) : IRequest<Result<long>>;

public class CrearPacienteCommandHandler : IRequestHandler<CrearPacienteCommand, Result<long>>
{
    private readonly IPacienteRepository _repo;
    private readonly Interfaces.Persistence.IUnitOfWork _unitOfWork;

    public CrearPacienteCommandHandler(IPacienteRepository repo, Interfaces.Persistence.IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<long>> Handle(CrearPacienteCommand request, CancellationToken cancellationToken)
    {
        var paciente = new Paciente(request.TipoDocumento, request.DocIdentidad, request.Nombres, request.ApellidoPaterno, request.ApellidoMaterno, request.Genero, request.Celular, request.Correo, request.FechaNacimiento, request.IdAlumno, request.IdFirma);
        var crearPacienteRequest = new UW.AdminPagoExpress.Shared.Request.CrearPacienteRequest
        {
            TipoDocumento = request.TipoDocumento,
            DocIdentidad = request.DocIdentidad,
            Nombres = request.Nombres,
            ApellidoPaterno = request.ApellidoPaterno,
            ApellidoMaterno = request.ApellidoMaterno,
            Genero = request.Genero,
            Celular = request.Celular,
            Correo = request.Correo,
            FechaNacimiento = request.FechaNacimiento,
            IdAlumno = request.IdAlumno,
            IdFirma = request.IdFirma
        };
        var id = await _repo.CrearAsync(crearPacienteRequest, cancellationToken);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);
        return Result.Success(id);
    }
}

public class CrearPacienteCommandValidator : AbstractValidator<CrearPacienteCommand>
{
    public CrearPacienteCommandValidator()
    {
        RuleFor(x => x.Nombres).NotEmpty().WithMessage("Nombres obligatorios");
        RuleFor(x => x.DocIdentidad).NotEmpty().WithMessage("Documento obligatorio");
    }
}

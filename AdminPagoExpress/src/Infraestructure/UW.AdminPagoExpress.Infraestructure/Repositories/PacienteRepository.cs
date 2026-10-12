using Microsoft.EntityFrameworkCore;
using UW.AdminPagoExpress.Application.Interfaces.Persistence;
using UW.AdminPagoExpress.Domain.Entities;
using UW.AdminPagoExpress.Shared.Response;
using UW.AdminPagoExpress.Shared.Request;
using UW.AdminPagoExpress.Infrastructure.Persistence;

namespace UW.AdminPagoExpress.Infrastructure.Repositories;

public class PacienteRepository : IPacienteRepository
{
    private readonly ApplicationDbContext _db;

    public PacienteRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<PacienteResponse>> ListarAsync(CancellationToken ct = default)
    {
        return await _db.Set<Paciente>()
            .Select(p => new PacienteResponse(
                p.Id,
                p.TipoDocumento,
                p.DocIdentidad,
                p.Nombres,
                p.ApellidoPaterno,
                p.ApellidoMaterno,
                p.Genero,
                p.Celular,
                p.Correo,
                p.FechaNacimiento,
                p.FechaCreacion,
                p.FechaModificacion,
                p.IdAlumno,
                p.IdFirma,
                "1"
            ))
            .ToListAsync(ct);
    }

    public async Task<PacienteResponse?> ObtenerPorIdAsync(long id, CancellationToken ct = default)
    {
        var p = await _db.Set<Paciente>().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (p == null) return null;
        return new PacienteResponse(
            p.Id,
            p.TipoDocumento,
            p.DocIdentidad,
            p.Nombres,
            p.ApellidoPaterno,
            p.ApellidoMaterno,
            p.Genero,
            p.Celular,
            p.Correo,
            p.FechaNacimiento,
            p.FechaCreacion,
            p.FechaModificacion,
            p.IdAlumno,
            p.IdFirma,
            "1"
        );
    }

    public async Task<long> CrearAsync(CrearPacienteRequest request, CancellationToken ct = default)
    {
        var paciente = Paciente.Crear(request.TipoDocumento, request.DocIdentidad, request.Nombres, request.ApellidoPaterno, request.ApellidoMaterno, request.Genero, request.Celular, request.Correo, request.FechaNacimiento ?? DateTime.UtcNow, request.IdAlumno, request.IdFirma);
        await _db.Set<Paciente>().AddAsync(paciente, ct);
        await _db.SaveChangesAsync(ct);
        return paciente.Id;
    }

    public async Task<bool> ActualizarAsync(long id, ActualizarPacienteRequest request, CancellationToken ct = default)
    {
        var p = await _db.Set<Paciente>().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (p == null) return false;
        p.Actualizar(request.TipoDocumento, request.DocIdentidad, request.Nombres, request.ApellidoPaterno, request.ApellidoMaterno, request.Genero, request.Celular, request.Correo, request.FechaNacimiento ?? p.FechaNacimiento, request.IdAlumno, request.IdFirma);
        _db.Set<Paciente>().Update(p);
        var changes = await _db.SaveChangesAsync(ct);
        return changes > 0;
    }

    public async Task<bool> EliminarAsync(long id, CancellationToken ct = default)
    {
        var p = await _db.Set<Paciente>().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (p == null) return false;
        _db.Set<Paciente>().Remove(p);
        var changes = await _db.SaveChangesAsync(ct);
        return changes > 0;
    }
}

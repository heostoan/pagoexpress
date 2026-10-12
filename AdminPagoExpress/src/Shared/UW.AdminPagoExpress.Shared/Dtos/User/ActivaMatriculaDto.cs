namespace UW.AdminPagoExpress.Shared.Dtos.User;

public class ActivaMatriculaDto
{
    public string Estado { get; set; } = default!;
    public string Mensaje { get; set; } = default!;
    public string Periodo { get; set; } = default!;
    public string PeriodoDescripcion { get; set; } = default!;
    public string Mes { get; set; } = default!;
    public string MesDescripcion { get; set; } = default!;
    public string Modalidad { get; set; } = default!;
}
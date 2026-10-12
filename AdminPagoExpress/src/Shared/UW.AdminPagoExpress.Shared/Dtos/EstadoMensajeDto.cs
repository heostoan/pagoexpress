namespace UW.AdminPagoExpress.Shared.Dtos;

public class EstadoMensajeDto
{
    public string estado { get; set; }

    public string mensaje { get; set; }
    public bool valido => estado == "1";

    public string codigoCurso { get; set; }
    public string descripcionCurso { get; set; }
}
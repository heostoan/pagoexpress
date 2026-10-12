namespace UW.AdminPagoExpress.Domain.Entities;

public class EstadoMensajeEntity
{
    public string estado { get; set; }

    public string mensaje { get; set; }
    public bool valido => estado == "1";

    public string codigoCurso { get; set; }
    public string descripcionCurso { get; set; }
}
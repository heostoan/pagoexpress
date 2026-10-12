namespace UW.AdminPagoExpress.Client.Components.Shared;

public class DataTableColumn<T>
{
    public string Titulo { get; set; } = string.Empty;

    /// <summary>
    /// Puede devolver string o RenderFragment. Si es RenderFragment, se renderiza como HTML.
    /// </summary>
    public Func<T, object> Valor { get; set; } = _ => string.Empty;

    /// <summary>
    /// Indica si el valor debe renderizarse como HTML crudo (badges, botones, etc.)
    /// </summary>
    public bool EsHtml { get; set; } = false;
}
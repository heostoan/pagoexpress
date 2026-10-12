using System.ComponentModel;
using System.Reflection;

namespace UW.AdminPagoExpress.Shared.Extensions;

/// <summary>
///     Contiene métodos de extensión para trabajar con enumeraciones (enum).
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    ///     Obtiene la descripción asociada a un valor de enumeración (enum) que tiene el atributo
    ///     <see cref="DescriptionAttribute" />.
    ///     Si no se encuentra una descripción, devuelve el nombre del valor del enum.
    /// </summary>
    /// <param name="value">Valor del enum al que se desea obtener su descripción.</param>
    /// <returns>Texto de la descripción si existe, o el nombre del enum como cadena si no hay descripción.</returns>
    /// <example>
    ///     Enum con descripción:
    ///     <code>
    /// public enum Estado
    /// {
    ///     [Description("Activo")]
    ///     Activo,
    ///     [Description("Inactivo")]
    ///     Inactivo
    /// }
    /// 
    /// var estado = Estado.Activo;
    /// var descripcion = estado.GetDescription(); // "Activo"
    /// </code>
    /// </example>
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

        return attribute?.Description ?? value.ToString();
    }
}
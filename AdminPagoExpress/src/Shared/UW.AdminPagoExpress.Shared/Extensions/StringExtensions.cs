using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace UW.AdminPagoExpress.Shared.Extensions;

/// <summary>
///     Proporciona métodos de extensión adicionales para trabajar con cadenas (<see cref="string" />).
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     Elimina las tildes (acentos) de una cadena.
    /// </summary>
    /// <param name="text">Texto de entrada.</param>
    /// <returns>Cadena sin caracteres acentuados.</returns>
    public static string QuitarTildes(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        var normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in normalized)
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    ///     Capitaliza cada palabra de la cadena, colocando en mayúscula solo la primera letra de cada palabra.
    /// </summary>
    /// <param name="text">Texto de entrada.</param>
    /// <returns>Cadena con cada palabra capitalizada.</returns>
    public static string CapitalizarPalabras(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        return string.Join(" ", text
            .ToLower()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(palabra => char.ToUpper(palabra[0]) + palabra.Substring(1)));
    }

    /// <summary>
    ///     Obtiene el texto que sigue después de un guion (" - ") en una cadena.
    ///     Si no se encuentra el guion, se devuelve la cadena original.
    /// </summary>
    /// <param name="text">Texto de entrada.</param>
    /// <returns>Texto después del guion, o la misma cadena si no existe guion.</returns>
    public static string ObtenerNombreSinGuionAdelante(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return "";

        var index = text.IndexOf(" - ");
        if (index != -1)
            return text[(index + 3)..].Trim();

        return text;
    }

    /// <summary>
    ///     Elimina la letra 'W' al inicio de la cadena si existe (ignorando mayúsculas/minúsculas).
    /// </summary>
    /// <param name="text">Texto de entrada.</param>
    /// <returns>Cadena sin la 'W' inicial.</returns>
    public static string QuitarWInicial(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return "";

        return text.StartsWith("W", StringComparison.OrdinalIgnoreCase)
            ? text[1..]
            : text;
    }

    /// <summary>
    ///     Normaliza una cadena eliminando tildes, espacios en blanco y convirtiéndola a minúsculas.
    /// </summary>
    /// <param name="input">Texto de entrada.</param>
    /// <returns>Cadena normalizada.</returns>
    public static string Normalizar(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        var normalized = input.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder();

        foreach (var c in normalized)
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                builder.Append(c);

        var withoutDiacritics = builder.ToString().Normalize(NormalizationForm.FormC);
        var withoutWhitespace = new string(withoutDiacritics.Where(c => !char.IsWhiteSpace(c)).ToArray());

        return withoutWhitespace.ToLowerInvariant();
    }

    /// <summary>
    ///     Intenta convertir una cadena en una fecha considerando múltiples formatos posibles.
    /// </summary>
    /// <param name="fechaTexto">Texto con la fecha a analizar.</param>
    /// <returns>Objeto <see cref="DateTime" /> si la conversión fue exitosa; de lo contrario, <c>null</c>.</returns>
    public static DateTime? ToParsedDate(this string fechaTexto)
    {
        if (string.IsNullOrWhiteSpace(fechaTexto))
            return null;

        string[] formatos =
        {
            "MM/dd/yyyy HH:mm:ss",
            "dd/MM/yyyy HH:mm:ss",
            "yyyy-MM-dd HH:mm:ss",
            "MM/dd/yyyy",
            "dd/MM/yyyy",
            "yyyy-MM-dd"
        };

        if (DateTime.TryParseExact(fechaTexto, formatos, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var fecha))
            return fecha;

        if (DateTime.TryParse(fechaTexto, out fecha))
            return fecha;

        return null;
    }

    /// <summary>
    ///     Da formato a una hora en formato "HHmm" convirtiéndola a "HH:mm".
    /// </summary>
    /// <param name="hora">Hora como cadena.</param>
    /// <returns>Cadena con formato "HH:mm" si es válido, de lo contrario la misma entrada.</returns>
    public static string FormatearHora(this string hora)
    {
        if (!string.IsNullOrWhiteSpace(hora) && hora.Length == 4 && int.TryParse(hora, out _))
            return $"{hora.Substring(0, 2)}:{hora.Substring(2, 2)}";

        return hora;
    }

    /// <summary>
    ///     Extrae el primer número entero encontrado en una cadena.
    /// </summary>
    /// <param name="text">Texto de entrada.</param>
    /// <returns>Primer número entero encontrado o <c>null</c> si no se encuentra ningún número.</returns>
    public static int? ExtractFirstNumber(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        var match = Regex.Match(text, @"\d+");
        return match.Success ? int.Parse(match.Value) : null;
    }
}
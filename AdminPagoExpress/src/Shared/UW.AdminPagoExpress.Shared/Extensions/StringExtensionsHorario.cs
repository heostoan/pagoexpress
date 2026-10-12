using System.Globalization;

namespace UW.AdminPagoExpress.Shared.Extensions;

/// <summary>
///     Proporciona métodos de extensión para trabajar con cadenas relacionadas a horarios y días de la semana.
/// </summary>
public static class StringExtensionsHorario
{
    /// <summary>
    ///     Convierte una hora en formato "HHmm" y un día abreviado (por ejemplo, "lun", "mar") en un <see cref="DateTime" />
    ///     correspondiente al próximo día indicado con la hora especificada.
    /// </summary>
    /// <param name="hora">Hora en formato "HHmm" (por ejemplo, "0930" para las 9:30 a. m.).</param>
    /// <param name="dia">Día de la semana en forma abreviada (ej. "lun", "mar", "mie").</param>
    /// <returns>Objeto <see cref="DateTime" /> correspondiente, o <c>null</c> si el formato es inválido.</returns>
    public static DateTime? ConvertirHoraYDia(this string hora, string dia)
    {
        if (string.IsNullOrEmpty(hora) || string.IsNullOrEmpty(dia))
            return null;

        dia = dia.ToLower().Trim();
        var diasSemana = new Dictionary<string, DayOfWeek>(StringComparer.OrdinalIgnoreCase)
        {
            { "lun", DayOfWeek.Monday },
            { "mar", DayOfWeek.Tuesday },
            { "mie", DayOfWeek.Wednesday },
            { "jue", DayOfWeek.Thursday },
            { "vie", DayOfWeek.Friday },
            { "sab", DayOfWeek.Saturday },
            { "dom", DayOfWeek.Sunday }
        };

        if (!diasSemana.TryGetValue(dia, out var diaSemana))
            return null;

        if (DateTime.TryParseExact(hora, "HHmm", null, DateTimeStyles.None, out var horaConvertida))
        {
            var fechaActual = DateTime.Now;
            var diferenciaDias = ((int)diaSemana - (int)fechaActual.DayOfWeek + 7) % 7;
            return fechaActual.Date.AddDays(diferenciaDias).Add(horaConvertida.TimeOfDay);
        }

        return null;
    }

    /// <summary>
    ///     Convierte una hora en formato "HH:mm" y un día completo (por ejemplo, "lunes", "martes") en un
    ///     <see cref="DateTime" /> correspondiente al próximo día indicado con la hora especificada.
    /// </summary>
    /// <param name="hora">Hora en formato "HH:mm" (por ejemplo, "09:30").</param>
    /// <param name="dia">Día de la semana completo (ej. "lunes", "martes").</param>
    /// <returns>Objeto <see cref="DateTime" /> correspondiente, o <c>null</c> si el formato es inválido.</returns>
    public static DateTime? ConvertirHoraYDiaExtendida(this string hora, string dia)
    {
        if (string.IsNullOrEmpty(hora) || string.IsNullOrEmpty(dia))
            return null;

        dia = dia.ToLower().Trim();
        var diasSemana = new Dictionary<string, DayOfWeek>(StringComparer.OrdinalIgnoreCase)
        {
            { "lunes", DayOfWeek.Monday },
            { "martes", DayOfWeek.Tuesday },
            { "miercoles", DayOfWeek.Wednesday },
            { "jueves", DayOfWeek.Thursday },
            { "viernes", DayOfWeek.Friday },
            { "sabado", DayOfWeek.Saturday },
            { "domingo", DayOfWeek.Sunday }
        };

        if (!diasSemana.TryGetValue(dia, out var diaSemana))
            return null;

        if (DateTime.TryParseExact(hora, "HH:mm", null, DateTimeStyles.None, out var horaConvertida))
        {
            var fechaActual = DateTime.Now;
            var diferenciaDias = ((int)diaSemana - (int)fechaActual.DayOfWeek + 7) % 7;
            return fechaActual.Date.AddDays(diferenciaDias).Add(horaConvertida.TimeOfDay);
        }

        return null;
    }

    /// <summary>
    ///     Convierte una cadena en formato "HHmm" a un objeto <see cref="TimeSpan" />.
    /// </summary>
    /// <param name="hora">Cadena de hora en formato "HHmm".</param>
    /// <returns><see cref="TimeSpan" /> con la hora y minutos especificados.</returns>
    /// <exception cref="FormatException">Se lanza si la cadena no está en el formato correcto.</exception>
    public static TimeSpan ToTimeSpan(this string hora)
    {
        if (string.IsNullOrEmpty(hora) || hora.Length != 4)
            throw new FormatException($"El formato de la hora '{hora}' no es válido. Debe ser 'HHmm'.");

        var horas = int.Parse(hora.Substring(0, 2));
        var minutos = int.Parse(hora.Substring(2, 2));

        return new TimeSpan(horas, minutos, 0);
    }
}
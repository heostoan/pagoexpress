using System.ComponentModel;

namespace UW.AdminPagoExpress.Shared.Enums;

public enum Color
{
    [Description("info-gen-fill")] Gris,
    [Description("info-success-fill")] Verde,
    [Description("info-success-sec")] VerdeClaro,
    [Description("info-danger-sec")] Rojo,
    [Description("info-gen-sec")] Azul,
    [Description("info-warning-sec")] Amarillo
}
using System.ComponentModel.DataAnnotations;

namespace UW.AdminPagoExpress.Shared.Dtos.User;

public class LoginSinPasswordDto
{
    [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
    public string Usuario { get; set; } = "";

    [Required(ErrorMessage = "Debe seleccionar una institución")]
    public string Institucion { get; set; } = "";

    [Required(ErrorMessage = "Debe seleccionar el tipo de usuario")]
    public string TipoUsuario { get; set; } = "";

    public string MatriculaIntegralCodigoUsuario { get; set; } = "";


    [Display(Name = "Remember me?")] public bool RememberMe { get; set; }
}
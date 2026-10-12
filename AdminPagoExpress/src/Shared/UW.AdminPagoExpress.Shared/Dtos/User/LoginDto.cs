using System.ComponentModel.DataAnnotations;

namespace UW.AdminPagoExpress.Shared.Dtos.User;

public class LoginDto
{
    [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
    public string Usuario { get; set; } = "";

    [Required(ErrorMessage = "Debe ingresar su contraseña.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Required] public string Institucion { get; set; } = "";

    [Required] public string TipoUsuario { get; set; } = "";

    [Display(Name = "Remember me?")] public bool RememberMe { get; set; }

    public string IdSesion { get; set; }
}
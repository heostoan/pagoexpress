namespace UW.AdminPagoExpress.Shared.Dtos.User;

public class ValidateAccessDto
{
    public bool IsValid { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string Estado { get; set; } = default!;
    public string Token { get; set; } = default!;
}
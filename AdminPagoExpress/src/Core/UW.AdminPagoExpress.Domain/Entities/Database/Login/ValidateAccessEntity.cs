namespace UW.AdminPagoExpress.Domain.Entities.Database.Login;

public class ValidateAccessEntity
{
    public bool IsValid { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string Estado { get; set; } = default!;
    public string Token { get; set; } = default!;
}
namespace UW.AdminPagoExpress.Domain.Interfaces.Repositories.Factory;

public interface IUserFactory
{
    IUserRepository CreateRepository(string useCloud);
}

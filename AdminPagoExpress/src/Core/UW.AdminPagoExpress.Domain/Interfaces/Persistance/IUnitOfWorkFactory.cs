namespace UW.AdminPagoExpress.Domain.Interfaces.Persistance;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create(string tipoUsuario, bool esMatricula = false);
}

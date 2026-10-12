using System.Data;

namespace UW.AdminPagoExpress.Domain.Interfaces.Persistance;

public interface IDbConnectionFactoryManager
{
    IDbConnection CreateConnection(string connectionName);
    IDbTransaction BeginTransaction(string connectionName);
    void CommitTransaction();
    void RollbackTransaction();
    void Dispose();
}
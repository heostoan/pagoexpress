using Microsoft.Extensions.Configuration;
using System.Data;

namespace UW.AdminPagoExpress.Domain.Interfaces.Persistance;

public interface IDbConnectionOciManager
{
    IDbConnection CreateConnection(string connectionName);
}

namespace UW.AdminPagoExpress.Domain.Interfaces.Repositories;

public interface IReadFileRepository
{
    Task<string> ReadReplaceDataOnfile(string ruta, Dictionary<string, string> variables);
}
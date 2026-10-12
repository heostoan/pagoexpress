using UW.AdminPagoExpress.Domain.Interfaces.Repositories;

namespace UW.AdminPagoExpress.Infraestructure.Repositories;

public class ReadFileRepository : IReadFileRepository
{
    public async Task<string> ReadReplaceDataOnfile(string filePath, Dictionary<string, string> variables)
    {
        var contenido = string.Empty;

        using (var reader = new StreamReader(filePath))
        {
            contenido = reader.ReadToEnd();
        }

        foreach (var variable in variables) contenido = contenido.Replace(variable.Key, variable.Value);

        return contenido;
    }
}
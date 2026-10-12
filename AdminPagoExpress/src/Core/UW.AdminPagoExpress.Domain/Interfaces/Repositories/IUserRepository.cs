using UW.AdminPagoExpress.Domain.Entities.Database.Login;
using UW.AdminPagoExpress.Domain.Entities.Database.User;

namespace UW.AdminPagoExpress.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<UserEntity> GetUserAdmin(string Institucion, string Tipo_Usuario, string Usuario, string Password);
    public Task<UserEntity> GetUser(string Institucion, string Tipo_Usuario, string Usuario, string Password);
    public Task<UserEntity> GetUserWithoutPass(string Institucion, string Tipo_Usuario, string Usuario);
    public Task<UserPhotoEntity> GetUserPhoto(string usuario);

    public Task<ValidateAccessEntity> ValidateAccess(
        string Institucion, 
        string Tipo_Usuario, 
        string Usuario,
        string Password, 
        string idSesion
        );

    public Task<ValidateAccessEntity> ValidateAccessWithoutPassword(
        string Institucion, 
        string Tipo_Usuario,
        string Usuario, 
        string idSesion
        );

    public Task<string> GetUserByEmail(string emailMicrosoft);
    Task<List<string>> GetUserEmail(string alumno);
}
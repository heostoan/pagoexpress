namespace UW.AdminPagoExpress.Shared.Endpoints;

public static class UserEndpoints
{
    public const string UserLogin = "/api/user/login";
    public const string UserLogout = "/api/user/logout";
    public const string RemoveAuthToken = "/api/user/removeAuthToken";
    public const string UserGet = "/api/user/get";
    public const string UserGetAdmin = "/api/user/getAdmin";
    public const string UserValidateAccess = "/api/user/validate-access";
    public const string UserValidateAccessWithoutPassword = "/api/user/validate-access-withoutPassword";
    public const string UserGetClaims = "/api/user/getClaims";
    public const string UserGetGraph = "/api/user/getGraph";
    public const string UserGetByEmail = "/api/user/getUserByEmail";
    public const string UserGetPhoto = "/api/user/getUserPhoto";
    public const string Ip = "/api/IP";
    public const string testApi = "/api/user/testApi";
}
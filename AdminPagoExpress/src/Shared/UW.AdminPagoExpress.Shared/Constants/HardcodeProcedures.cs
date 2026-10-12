namespace UW.AdminPagoExpress.Shared.Constants;

public class HardcodeProcedures
{
    public const string TABLE = "USR_ODA";
    public const string PACKAGE = $"{TABLE}.PKG_CHATBOX";
    public const string RETURNCLOB = "PS_JSON";


    public const string GET_CATEGORIES = $"{PACKAGE}.SP_CATEGORIES";
    public const string GET_CATEGORY = $"{PACKAGE}.SP_CATEGORY";
    public const string PUT_CATEGORY = $"{PACKAGE}.SP_MODIFY_CATEGORY";
    public const string DELETE_CATEGORY = $"{PACKAGE}.SP_DELETE_CATEGORY";
}

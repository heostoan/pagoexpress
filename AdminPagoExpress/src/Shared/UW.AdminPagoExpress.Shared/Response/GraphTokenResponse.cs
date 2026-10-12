namespace UW.AdminPagoExpress.Shared.Response;

public class GraphTokenResponse
{
    //SUCCESS
    public string scope { get; set; }
    public int expires_in { get; set; }
    public int ext_expires_in { get; set; }
    public string token_type { get; set; }

    public string access_token { get; set; }

    //ERROR
    public string error { get; set; }
    public string error_description { get; set; }
    public string timestamp { get; set; }
    public string trace_id { get; set; }
    public string correlation_id { get; set; }
    public string error_uri { get; set; }
}
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Xml;
using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using RestSharp;
using UW.AdminPagoExpress.Application.Interfaces;
using UW.AdminPagoExpress.Shared.Dtos;
using UW.AdminPagoExpress.Shared.Response;

namespace UW.AdminPagoExpress.Infrastructure.Services;

public class CorreoService : ICorreoService
{
    private readonly EnvioCorreoConfigDto _configEnvioCorreo;
    private readonly IConfiguration _configuration;
    private readonly GraphServiceClient _graphClient;
    private readonly string _senderEmail;
    private readonly string contraseña = "D@021660773876ox";
    private readonly string emailRemitente = "matricula@uwiener.edu.pe";
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly string nombreRemitente = "Matricula Universidad Wiener";
    private readonly int smtpPort = 587;

    private readonly string smtpServer = "smtp.office365.com";

    public CorreoService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor,
        IOptions<EnvioCorreoConfigDto> configEnvioCorreo)
    {
        _configEnvioCorreo = configEnvioCorreo.Value;
        _configuration = configuration;

        this.httpContextAccessor = httpContextAccessor;

        var tenantId = configuration["GraphSettings:IdTenant"];
        var clientId = configuration["GraphSettings:IdApp"];
        var clientSecret = configuration["GraphSettings:ClientSecret"];
        var scope = configuration["GraphSettings:Scope"];
        _senderEmail = configuration["GraphSettings:Username"]; // Correo del usuario que enviará los emails

        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
        _graphClient = new GraphServiceClient(credential, new[] { scope });
    }

    public async Task<bool> EnviarCorreoUsuarioAsync(string destinatario, string asunto, string cuerpo,
        List<string> bccDestinatarios = null)
    {
        var idSesion = httpContextAccessor.HttpContext?.User?.FindFirst("IdSesion")?.Value;
        var user = httpContextAccessor.HttpContext?.User?.FindFirst("User")?.Value;

        try
        {
            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.Credentials = new NetworkCredential(emailRemitente, contraseña);
                client.EnableSsl = true;

                using (var mensaje = new MailMessage())
                {
                    mensaje.From = new MailAddress(emailRemitente, nombreRemitente);

                    if (!_configEnvioCorreo.Test)
                        mensaje.To.Add(new MailAddress(destinatario));
                    else
                        foreach (var correo in _configEnvioCorreo.CorreosTest)
                            mensaje.To.Add(new MailAddress(correo));

                    if (bccDestinatarios != null)
                        foreach (var bcc in bccDestinatarios)
                            mensaje.Bcc.Add(new MailAddress(bcc));

                    mensaje.Subject = asunto;
                    mensaje.Body = cuerpo;
                    mensaje.IsBodyHtml = true;

                    if (_configEnvioCorreo.Activo)
                    {
                        await client.SendMailAsync(mensaje);
                        Log.ForContext("IdSesion", idSesion).ForContext("Usuario", user).Information(
                            $"El envio de correos esta desactivado - Alumno: {user}");
                    }
                }
            }

            Log.ForContext("IdSesion", idSesion).ForContext("Usuario", user).Information(
                $"Correo enviado exitosamente - Alumno: {user}");
            return true;
        }
        catch (SmtpException ex)
        {
            Log.ForContext("IdSesion", idSesion).ForContext("Usuario", user).Information(
                $"Error al enviar correo: error - {ex.StatusCode} - Alumno: {user}");

            throw;
        }
    }

    public async Task<bool> EnviarCorreoUsuarioApiSMTPAsync(string Proyecto, string Destinatario, string Asunto,
        string Cuerpo, List<string> bccDestinatarios = null)
    {
        var idSesion = httpContextAccessor.HttpContext?.User?.FindFirst("IdSesion")?.Value;
        var user = httpContextAccessor.HttpContext?.User?.FindFirst("User")?.Value;

        try
        {
            /*GENERAR LLAVE PUBLICA*/
            var uriApiMailPublicKey = _configuration["UriApiMail"] + "/api/publicKey";
            var optionsPK = new RestClientOptions(new Uri(uriApiMailPublicKey))
            {
                MaxTimeout = -1
            };
            var clientPK = new RestClient(optionsPK);
            var call_ws_PK = new RestRequest(new Uri(uriApiMailPublicKey));
            var responseWs_PK = await clientPK.ExecuteAsync(call_ws_PK);
            var xmlRaw = JsonConvert.DeserializeObject<string>(responseWs_PK.Content);

            if (!responseWs_PK.IsSuccessful || string.IsNullOrEmpty(responseWs_PK.Content))
                throw new Exception("Error obteniendo clave pública");

            /* CARGAR CLAVE PUBLICA RSA */
            var rsa = RSA.Create();
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlRaw);

                var modulus = Convert.FromBase64String(xmlDoc.SelectSingleNode("//Modulus")?.InnerText ?? "");
                var exponent = Convert.FromBase64String(xmlDoc.SelectSingleNode("//Exponent")?.InnerText ?? "");

                var rsaKeyInfo = new RSAParameters
                {
                    Modulus = modulus,
                    Exponent = exponent
                };
                rsa.ImportParameters(rsaKeyInfo);
            }
            catch (Exception ex)
            {
                Log.Error("Error al cargar clave pública RSA: {Error}", ex.Message);
                throw;
            }

            /* Preparar JSON plano */
            var request = new
            {
                proyecto = Proyecto,
                remitente = nombreRemitente,
                destinatario = Destinatario,
                asunto = Asunto,
                mensaje = Cuerpo,
                copias = bccDestinatarios
            };

            var body = JsonConvert.SerializeObject(request);
            var plainBytes = Encoding.UTF8.GetBytes(body);

            /* CIFRADO HÍBRIDO: AES + RSA */
            string payloadJson;
            try
            {
                using var aes = Aes.Create();
                aes.GenerateKey();
                aes.GenerateIV();

                using var encryptor = aes.CreateEncryptor();
                var encryptedJson = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                var encryptedKey = rsa.Encrypt(aes.Key, RSAEncryptionPadding.Pkcs1);
                var encryptedIV = rsa.Encrypt(aes.IV, RSAEncryptionPadding.Pkcs1);

                var payload = new
                {
                    Key = Convert.ToBase64String(encryptedKey),
                    IV = Convert.ToBase64String(encryptedIV),
                    Data = Convert.ToBase64String(encryptedJson)
                };

                payloadJson = JsonConvert.SerializeObject(payload);
            }
            catch (Exception ex)
            {
                Log.Error("Error en cifrado híbrido: {Error}", ex.Message);
                throw;
            }

            /* Preparar petición POST para enviar el Mail*/
            var uriApiMail = _configuration["UriApiMail"] + "/api/sendMailSMTP";

            var options = new RestClientOptions(new Uri(uriApiMail))
            {
                MaxTimeout = -1
            };

            var client = new RestClient(options);
            var call_ws = new RestRequest(new Uri(uriApiMail), Method.Post);


            call_ws.AddHeader("Content-Type", "application/json");
            call_ws.AddStringBody(payloadJson, DataFormat.Json);

            var responseWs = await client.ExecuteAsync(call_ws);
            var responseData = responseWs.Content;

            if (responseWs.IsSuccessful && responseData == "true")
            {
                Log.ForContext("IdSesion", idSesion).ForContext("Usuario", user).Information(
                    $"Correo enviado exitosamente - Alumno: {user}");
                return true;
            }

            Log.ForContext("IdSesion", idSesion).ForContext("Usuario", user).Information(
                $"Correo no se ha podido enviar - Alumno: {user}");
            return false;
        }
        catch (SmtpException ex)
        {
            Log.ForContext("IdSesion", idSesion).ForContext("Usuario", user).Information(
                $"Error al enviar correo: error - {ex.StatusCode} - Alumno: {user}");

            throw;
        }
    }

    public async Task<bool> EnviarCorreoUsuarioApiGraphAsync(string Proyecto, string Destinatario, string Asunto,
        string Cuerpo, List<string> bccDestinatarios = null)
    {
        var idSesion = httpContextAccessor.HttpContext?.User?.FindFirst("IdSesion")?.Value;
        var user = httpContextAccessor.HttpContext?.User?.FindFirst("User")?.Value;

        try
        {
            /*GENERAR LLAVE PUBLICA - Secciones Adicionales*/
            var uriApiMailPublicKey = _configuration["UriApiMail"] + "/api/publicKey";
            var optionsPK = new RestClientOptions(new Uri(uriApiMailPublicKey))
            {
                MaxTimeout = -1
            };
            var clientPK = new RestClient(optionsPK);
            var call_ws_PK = new RestRequest(new Uri(uriApiMailPublicKey));
            var responseWs_PK = await clientPK.ExecuteAsync(call_ws_PK);
            var xmlRaw = JsonConvert.DeserializeObject<string>(responseWs_PK.Content);

            if (!responseWs_PK.IsSuccessful || string.IsNullOrEmpty(responseWs_PK.Content))
                throw new Exception("Error obteniendo clave pública");

            /* CARGAR CLAVE PUBLICA RSA */
            var rsa = RSA.Create();
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlRaw);

                var modulus = Convert.FromBase64String(xmlDoc.SelectSingleNode("//Modulus")?.InnerText ?? "");
                var exponent = Convert.FromBase64String(xmlDoc.SelectSingleNode("//Exponent")?.InnerText ?? "");

                var rsaKeyInfo = new RSAParameters
                {
                    Modulus = modulus,
                    Exponent = exponent
                };
                rsa.ImportParameters(rsaKeyInfo);
            }
            catch (Exception ex)
            {
                Log.Error("Error al cargar clave pública RSA: {Error}", ex.Message);
                throw;
            }

            /*GENERAR TOKEN GRAPH*/
            var uriApiMailTokenGraph = _configuration["UriApiMail"] + "/api/getTokenGraph";
            var optionsTK = new RestClientOptions(new Uri(uriApiMailTokenGraph))
            {
                MaxTimeout = -1
            };
            var clientTK = new RestClient(optionsTK);
            var call_ws_TK = new RestRequest(new Uri(uriApiMailTokenGraph), Method.Post);
            var responseWs_TK = await clientTK.ExecuteAsync(call_ws_TK);
            var graphTokenResponse = JsonConvert.DeserializeObject<GraphTokenResponse>(responseWs_TK.Content);

            /* Preparar JSON plano */
            var request = new
            {
                proyecto = Proyecto,
                token = graphTokenResponse.access_token,
                destinatario = Destinatario,
                asunto = Asunto,
                mensaje = Cuerpo,
                copias = bccDestinatarios
            };

            var body = JsonConvert.SerializeObject(request);
            var plainBytes = Encoding.UTF8.GetBytes(body);

            /* CIFRADO HÍBRIDO: AES + RSA */
            string payloadJson;
            try
            {
                using var aes = Aes.Create();
                aes.GenerateKey();
                aes.GenerateIV();

                using var encryptor = aes.CreateEncryptor();
                var encryptedJson = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                var encryptedKey = rsa.Encrypt(aes.Key, RSAEncryptionPadding.Pkcs1);
                var encryptedIV = rsa.Encrypt(aes.IV, RSAEncryptionPadding.Pkcs1);

                var payload = new
                {
                    Key = Convert.ToBase64String(encryptedKey),
                    IV = Convert.ToBase64String(encryptedIV),
                    Data = Convert.ToBase64String(encryptedJson)
                };

                payloadJson = JsonConvert.SerializeObject(payload);
            }
            catch (Exception ex)
            {
                Log.Error("Error en cifrado híbrido: {Error}", ex.Message);
                throw;
            }

            /* Preparar petición POST para enviar el Mail*/
            var uriApiMail = _configuration["UriApiMail"] + "/api/sendMailGraph";

            var options = new RestClientOptions(new Uri(uriApiMail))
            {
                MaxTimeout = -1
            };

            var client = new RestClient(options);
            var call_ws = new RestRequest(new Uri(uriApiMail), Method.Post);


            call_ws.AddHeader("Content-Type", "application/json");
            call_ws.AddStringBody(payloadJson, DataFormat.Json);

            var responseWs = await client.ExecuteAsync(call_ws);
            var responseData = responseWs.Content;

            if (responseWs.IsSuccessful && responseData == "true")
            {
                Log.ForContext("IdSesion", idSesion).ForContext("Usuario", user).Information(
                    $"Correo enviado exitosamente - Alumno: {user}");
                return true;
            }

            Log.ForContext("IdSesion", idSesion).ForContext("Usuario", user).Information(
                $"Correo no se ha podido enviar - Alumno: {user}");
            return false;
        }
        catch (SmtpException ex)
        {
            Log.ForContext("IdSesion", idSesion).ForContext("Usuario", user).Information(
                $"Error al enviar correo: error - {ex.StatusCode} - Alumno: {user}");

            throw;
        }
    }

    public bool GetEnviarCorreo()
    {
        return _configuration.GetValue<bool>("EnvioCorreo:Activo");
    }

    public bool GetTestCorreo()
    {
        return _configuration.GetValue<bool>("EnvioCorreo:Test");
    }

    public string GetCorreosDePrueba()
    {
        return _configuration.GetSection("EnvioCorreo:CorreosTest:0").Value;
    }
}
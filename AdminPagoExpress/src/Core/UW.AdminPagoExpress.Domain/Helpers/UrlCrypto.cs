using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace UW.AdminPagoExpress.Domain.Helpers;

public class UrlCrypto
{
    public readonly string IV;

    public readonly string Key;

    public UrlCrypto(IConfiguration configuration)
    {
        // Obtener valores de Key e IV desde la configuración
        //Key = configuration["EncryptionSettings:Key"]!;
        //IV = configuration["EncryptionSettings:IV"]!;
        Key = "V3r$8fL!qXz9bJ2A";
        IV = "1a2B3c4D5e6F7g8H";
    }

    public string EncryptUrl(string text)
    {
        using (var encryptor = Aes.Create())
        {
            encryptor.Mode = CipherMode.CBC;
            encryptor.Key = Encoding.UTF8.GetBytes(Key);
            encryptor.IV = Encoding.UTF8.GetBytes(IV);
            encryptor.Padding = PaddingMode.PKCS7;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(text);
                    }
                }

                var encryptedBytes = ms.ToArray();
                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }

    public string DecryptUrlParams(string encryptedText)
    {
        // Decodificar el Base64 primero
        encryptedText = encryptedText.Replace(" ", "+"); // Por si acaso hay espacios en la URL
        var cipherBytes = Convert.FromBase64String(encryptedText);

        using (var encryptor = Aes.Create())
        {
            encryptor.Mode = CipherMode.CBC;
            encryptor.Key = Encoding.UTF8.GetBytes(Key);
            encryptor.IV = Encoding.UTF8.GetBytes(IV);
            encryptor.Padding = PaddingMode.PKCS7;

            using (var ms = new MemoryStream(cipherBytes))
            {
                using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (var reader = new StreamReader(cs))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }


    public string DecryptTextAspClasic(string encryptedBase64)
    {
        if (encryptedBase64.StartsWith("77u/")) encryptedBase64 = encryptedBase64.Substring(4);

        // Decodificamos el base64
        var encryptedBytes = Convert.FromBase64String(encryptedBase64);

        // Convertimos a string usando UTF-8
        var encryptedText = Encoding.UTF8.GetString(encryptedBytes);

        // Preparamos el resultado
        var decrypted = new char[encryptedText.Length];
        var keyLength = Key.Length;

        // Aplicamos la operación XOR inversa
        for (var i = 0; i < encryptedText.Length; i++)
        {
            int keyChar = Key[i % keyLength];
            int encChar = encryptedText[i];
            decrypted[i] = (char)(encChar ^ keyChar);
        }

        return new string(decrypted);
    }
}
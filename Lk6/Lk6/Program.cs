using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        string password = "YourPassword";
        string salt = GenerateSalt();
        int iterations = 10000; // змініть на ваш номер варіанта * 10000

        byte[] key = GenerateKey(password, salt, iterations);
        byte[] iv = GenerateIV();

        string originalText = "Hello, encryption!";
        byte[] encrypted = EncryptAes(originalText, key, iv);
        string decrypted = DecryptAes(encrypted, key, iv);

        Console.WriteLine($"Original: {originalText}");
        Console.WriteLine($"Encrypted: {Convert.ToBase64String(encrypted)}");
        Console.WriteLine($"Decrypted: {decrypted}");
    }

    static byte[] GenerateKey(string password, string salt, int iterations)
    {
        using (var deriveBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), iterations))
        {
            return deriveBytes.GetBytes(256 / 8); // 256 bits for AES-256
        }
    }

    static byte[] GenerateIV()
    {
        using (var aes = new AesCryptoServiceProvider())
        {
            aes.GenerateIV();
            return aes.IV;
        }
    }

    static byte[] EncryptAes(string plainText, byte[] key, byte[] iv)
    {
        using (var aes = new AesCryptoServiceProvider())
        {
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encrypted;
            using (var msEncrypt = new System.IO.MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
                encrypted = msEncrypt.ToArray();
            }
            return encrypted;
        }
    }

    static string DecryptAes(byte[] cipherText, byte[] key, byte[] iv)
    {
        using (var aes = new AesCryptoServiceProvider())
        {
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            string decrypted;
            using (var msDecrypt = new System.IO.MemoryStream(cipherText))
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
            {
                decrypted = srDecrypt.ReadToEnd();
            }
            return decrypted;
        }
    }

    static string GenerateSalt()
    {
        byte[] salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        return Convert.ToBase64String(salt);
    }
}

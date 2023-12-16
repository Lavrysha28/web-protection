using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        // Генеруємо ключі
        using (var rsa = new RSACryptoServiceProvider())
        {
            // Зберігаємо відкритий ключ у файл
            SavePublicKeyToFile("publicKey.xml", rsa.ToXmlString(false));

            // Зберігаємо закритий ключ у файл 
            SavePrivateKeyToFile("privateKey.xml", rsa.ToXmlString(true));
        }

        // Завантажуємо відкритий ключ з файлу
        string publicKey = LoadPublicKeyFromFile("publicKey.xml");

        // Зашифровуємо повідомлення
        string message = "Hello, RSA!";
        byte[] encryptedMessage = EncryptMessage(message, publicKey);

        Console.WriteLine($"Original message: {message}");
        Console.WriteLine($"Encrypted message: {Convert.ToBase64String(encryptedMessage)}");

        // Завантажуємо закритий ключ (приклад для демонстрації)
        string privateKey = LoadPrivateKeyFromFile("privateKey.xml");

        // Розшифровуємо повідомлення
        string decryptedMessage = DecryptMessage(encryptedMessage, privateKey);

        Console.WriteLine($"Decrypted message: {decryptedMessage}");
    }

    static void SavePublicKeyToFile(string fileName, string publicKey)
    {
        File.WriteAllText(fileName, publicKey);
    }

    static string LoadPublicKeyFromFile(string fileName)
    {
        return File.ReadAllText(fileName);
    }

    static void SavePrivateKeyToFile(string fileName, string privateKey)
    {
        File.WriteAllText(fileName, privateKey);
    }

    static string LoadPrivateKeyFromFile(string fileName)
    {
        return File.ReadAllText(fileName);
    }

    static byte[] EncryptMessage(string message, string publicKey)
    {
        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(publicKey);
            return rsa.Encrypt(Encoding.UTF8.GetBytes(message), false);
        }
    }

    static string DecryptMessage(byte[] encryptedMessage, string privateKey)
    {
        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(privateKey);
            byte[] decryptedBytes = rsa.Decrypt(encryptedMessage, false);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}

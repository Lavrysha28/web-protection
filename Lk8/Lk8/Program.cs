using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        // Генеруємо пару ключів RSA 2048 біт
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            // Зберігаємо відкритий ключ у файл
            SavePublicKeyToFile("publicKey.xml", rsa.ToXmlString(false));

            // Зберігаємо закритий ключ у файл (це робиться лише для демонстрації, не рекомендується в реальних сценаріях)
            SavePrivateKeyToFile("privateKey.xml", rsa.ToXmlString(true));

            // Введене повідомлення для шифрування
            string message = "Hello, RSA!";

            // Зашифровуємо повідомлення
            byte[] encryptedMessage = EncryptMessage(message, "publicKey.xml");

            Console.WriteLine($"Original message: {message}");
            Console.WriteLine($"Encrypted message: {Convert.ToBase64String(encryptedMessage)}");

            // Розшифровуємо повідомлення
            string decryptedMessage = DecryptMessage(encryptedMessage, "privateKey.xml");

            Console.WriteLine($"Decrypted message: {decryptedMessage}");
        }
    }

    static void SavePublicKeyToFile(string fileName, string publicKey)
    {
        File.WriteAllText(fileName, publicKey);
    }

    static void SavePrivateKeyToFile(string fileName, string privateKey)
    {
        File.WriteAllText(fileName, privateKey);
    }

    static byte[] EncryptMessage(string message, string publicKeyFileName)
    {
        string publicKey = File.ReadAllText(publicKeyFileName);

        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(publicKey);
            return rsa.Encrypt(Encoding.UTF8.GetBytes(message), false);
        }
    }

    static string DecryptMessage(byte[] encryptedMessage, string privateKeyFileName)
    {
        string privateKey = File.ReadAllText(privateKeyFileName);

        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(privateKey);
            byte[] decryptedBytes = rsa.Decrypt(encryptedMessage, false);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}

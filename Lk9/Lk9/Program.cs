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

            // Зберігаємо закритий ключ у контейнер
            SavePrivateKeyToContainer(rsa, "MyKeyContainer");

            // Введене повідомлення для підписування
            string message = "Hello, digital signature!";

            // Підписуємо повідомлення
            byte[] signature = SignMessage(message, "MyKeyContainer");

            Console.WriteLine($"Original message: {message}");
            Console.WriteLine($"Digital signature: {Convert.ToBase64String(signature)}");

            // Перевіряємо підпис
            bool isSignatureValid = VerifySignature(message, signature, "publicKey.xml");

            Console.WriteLine($"Signature is valid: {isSignatureValid}");
        }
    }

    static void SavePublicKeyToFile(string fileName, string publicKey)
    {
        File.WriteAllText(fileName, publicKey);
    }

    static void SavePrivateKeyToContainer(RSACryptoServiceProvider rsa, string containerName)
    {
        CspParameters cspParams = new CspParameters
        {
            KeyContainerName = containerName,
            Flags = CspProviderFlags.UseMachineKeyStore // Використовуємо загальний сховище ключів
        };

        var rsaKey = new RSACryptoServiceProvider(cspParams);
    }

    static byte[] SignMessage(string message, string containerName)
    {
        CspParameters cspParams = new CspParameters
        {
            KeyContainerName = containerName,
            Flags = CspProviderFlags.UseMachineKeyStore // Використовуємо загальний сховище ключів
        };

        using (var rsa = new RSACryptoServiceProvider(cspParams))
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] signature = rsa.SignData(messageBytes, new SHA256Managed());
            return signature;
        }
    }

    static bool VerifySignature(string message, byte[] signature, string publicKeyFileName)
    {
        string publicKey = File.ReadAllText(publicKeyFileName);

        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(publicKey);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            return rsa.VerifyData(messageBytes, new SHA256Managed(), signature);
        }
    }
}

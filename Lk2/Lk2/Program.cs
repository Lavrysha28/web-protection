using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        // Шлях до вхiдного файлу
        string inputFilePath = "E:\\Visual Studio project\\Protect\\Lk2\\encfile.dat";

        // Зчитування вмiсту файлу
        byte[] fileData = File.ReadAllBytes(inputFilePath);

        // Генерацiя випадкового ключа та його використання для шифрування
        byte[] key = GenerateRandomKey(fileData.Length);
        byte[] encryptedData = Encrypt(fileData, key);

        // Створення шляху для вихiдного зашифрованого файлу з розширенням ".dat"
        string outputFilePath = Path.ChangeExtension(inputFilePath, "dat");

        // Запис зашифрованого вмiсту у файл
        File.WriteAllBytes(outputFilePath, encryptedData);

        Console.WriteLine("Файл успiшно зашифровано i збережено в {0}", outputFilePath);
    }

    // Метод для генерацiї випадкового ключа
    static byte[] GenerateRandomKey(int length) 
    {
        byte[] key = new byte[length];
        new Random().NextBytes(key);
        return key;
    }

    // Метод для шифрування вмiсту використовуючи XOR
    static byte[] Encrypt(byte[] data, byte[] key)
    {
        byte[] encryptedData = new byte[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            encryptedData[i] = (byte)(data[i] ^ key[i]);
        }
        return encryptedData;
    }
}

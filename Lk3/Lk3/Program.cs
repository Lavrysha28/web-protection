using System;
using System.Security.Cryptography;
using System.Text;

class Програма
{
    static void Main()
    {
        // Вхiднi данi
        string вхiднiДанi1 = "Привiт, свiт!";
        string вхiднiДанi2 = "Привiт, свiт?";

        // Обчислення хеш-кодiв за рiзними алгоритмами
        ОбчислитиiПорiвнятиХешi(вхiднiДанi1, вхiднiДанi2, new MD5CryptoServiceProvider(), "MD5");
        ОбчислитиiПорiвнятиХешi(вхiднiДанi1, вхiднiДанi2, new SHA1CryptoServiceProvider(), "SHA-1");
        ОбчислитиiПорiвнятиХешi(вхiднiДанi1, вхiднiДанi2, new SHA256CryptoServiceProvider(), "SHA-256");
        ОбчислитиiПорiвнятиХешi(вхiднiДанi1, вхiднiДанi2, new SHA384CryptoServiceProvider(), "SHA-384");
        ОбчислитиiПорiвнятиХешi(вхiднiДанi1, вхiднiДанi2, new SHA512CryptoServiceProvider(), "SHA-512");

        Console.ReadLine();
    }

    static void ОбчислитиiПорiвнятиХешi(string данi1, string данi2, HashAlgorithm алгоритм, string iмЯАлгоритму)
    {
        // Обчислення хеш-коду для першого набору даних
        byte[] хеш1 = ОбчислитиХеш(алгоритм, данi1);

        // Обчислення хеш-коду для другого набору даних
        byte[] хеш2 = ОбчислитиХеш(алгоритм, данi2);

        // Порiвняння розмiрiв хеш-кодiв
        Console.WriteLine($"{iмЯАлгоритму} Розмiри хеш-кодiв:");
        Console.WriteLine($"Данi1: {хеш1.Length} байтiв, Данi2: {хеш2.Length} байтiв");

        // Порiвняння значень хеш-кодiв
        Console.WriteLine($"{iмЯАлгоритму} Значення хеш-кодiв:");
        Console.WriteLine($"Данi1: {BitConverter.ToString(хеш1).Replace("-", "")}");
        Console.WriteLine($"Данi2: {BitConverter.ToString(хеш2).Replace("-", "")}");
        Console.WriteLine();
    }

    static byte[] ОбчислитиХеш(HashAlgorithm алгоритм, string данi)
    {
        byte[] данiБайти = Encoding.UTF8.GetBytes(данi);
        return алгоритм.ComputeHash(данiБайти);
    }
}

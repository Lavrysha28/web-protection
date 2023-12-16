using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

class Program
{
    private static Dictionary<string, string> реєстрацiя = new Dictionary<string, string>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Оберiть опцiю:");
            Console.WriteLine("1. Реєстрацiя");
            Console.WriteLine("2. Авторизацiя");
            Console.WriteLine("3. Вихiд");

            string вибiр = Console.ReadLine();

            switch (вибiр)
            {
                case "1":
                    Реєстрацiя();
                    break;
                case "2":
                    Авторизацiя();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неправильний вибiр. Спробуйте ще раз.");
                    break;
            }
        }
    }

    static void Реєстрацiя()
    {
        Console.Write("Введiть логiн: ");
        string логiн = Console.ReadLine();

        if (реєстрацiя.ContainsKey(логiн))
        {
            Console.WriteLine("Такий логiн вже iснує. Спробуйте вибрати iнший.");
            return;
        }

        Console.Write("Введiть пароль: ");
        string пароль = Console.ReadLine();

        string хешПароля = ОбчислитиХеш(пароль);

        реєстрацiя.Add(логiн, хешПароля);

        Console.WriteLine("Реєстрацiя пройшла успiшно!");
    }

    static void Авторизацiя()
    {
        Console.Write("Введiть логiн: ");
        string логiн = Console.ReadLine();

        if (!реєстрацiя.ContainsKey(логiн))
        {
            Console.WriteLine("Логiн не знайдено. Спробуйте зареєструватися.");
            return;
        }

        Console.Write("Введiть пароль: ");
        string пароль = Console.ReadLine();

        string введенийХеш = ОбчислитиХеш(пароль);

        if (реєстрацiя[логiн] == введенийХеш)
        {
            Console.WriteLine("Авторизацiя успiшна!");
        }
        else
        {
            Console.WriteLine("Неправильний пароль. Спробуйте ще раз.");
        }
    }

    static string ОбчислитиХеш(string вхiд)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] хешBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(вхiд));
            return BitConverter.ToString(хешBytes).Replace("-", "").ToLower();
        }
    }
}

using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

class Програма
{
    static void Main()
    {
        // Генерацiя секретного ключа
        byte[] секретнийКлюч = ГенеруватиВипадковийКлюч();

        // Введення повiдомлення для автентифiкацiї
        string повiдомлення = "Привiт, свiт!";

        // Обчислення HMAC
        byte[] hmac = ОбчислитиHmacSha256(Encoding.UTF8.GetBytes(повiдомлення), секретнийКлюч);

        // Перевiрка автентичностi повiдомлення
        bool чиПовiдомленняАутентичне = ПеревiритиHmacSha256(Encoding.UTF8.GetBytes(повiдомлення), hmac, секретнийКлюч);

        Console.WriteLine("HMAC: " + Convert.ToBase64String(hmac));
        Console.WriteLine("Чи Повiдомлення Аутентичне? " + чиПовiдомленняАутентичне);
    }

    // Генерацiя випадкового секретного ключа
    static byte[] ГенеруватиВипадковийКлюч()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] ключ = new byte[32]; // Використовуйте довжину, пiдходящу для конкретного алгоритму (наприклад, 16 байт для HMACSHA256)
            rng.GetBytes(ключ);
            return ключ;
        }
    }

    // Обчислення HMACSHA256
    static byte[] ОбчислитиHmacSha256(byte[] данi, byte[] ключ)
    {
        using (var hmacSha256 = new HMACSHA256(ключ))
        {
            return hmacSha256.ComputeHash(данi);
        }
    }

    // Перевiрка автентичностi HMACSHA256
    static bool ПеревiритиHmacSha256(byte[] данi, byte[] хеш, byte[] ключ)
    {
        byte[] обчисленийХеш = ОбчислитиHmacSha256(данi, ключ);

        // Порiвняння обчисленого HMAC iз заздалегiдь отриманим HMAC
        return StructuralComparisons.StructuralEqualityComparer.Equals(обчисленийХеш, хеш);
    }
}

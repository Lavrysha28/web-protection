using System;
using System.Security.Cryptography;
using System.Text;

class SaltedHash
{
    private const int SaltSize = 32; // Довжина "солi" в байтах

    public static (string hashedPassword, string salt) HashPassword(string password)
    {
        // Генерацiя випадкової "солi"
        byte[] salt;
        using (var rng = new RNGCryptoServiceProvider())
        {
            salt = new byte[SaltSize];
            rng.GetBytes(salt);
        }

        // Обчислення хешу пароля разом iз "солею"
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
        {
            byte[] hashBytes = pbkdf2.GetBytes(32); // 32 байти для 256-бiтного ключа
            string hashedPassword = Convert.ToBase64String(hashBytes);
            string saltString = Convert.ToBase64String(salt);
            return (hashedPassword, saltString);
        }
    }

    public static bool VerifyPassword(string password, string hashedPassword, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);

        // Перевiрка хешу пароля з використанням "солi"
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
        {
            byte[] hashBytes = pbkdf2.GetBytes(32); // 32 байти для 256-бiтного ключа
            string computedHash = Convert.ToBase64String(hashBytes);
            return computedHash == hashedPassword;
        }
    }
}

class Program
{
    static void Main()
    {
        // Демонстрацiя роботи класу SaltedHash
        Console.WriteLine("Демонстрацiя роботи класу SaltedHash:");
        var (hashedPassword, salt) = SaltedHash.HashPassword("mySecurePassword");
        Console.WriteLine($"Хеш пароля: {hashedPassword}");
        Console.WriteLine($"Сiль: {salt}");

        // Реєстрацiя та автентифiкацiя користувача
        Console.WriteLine("\nРеєстрацiя та автентифiкацiя користувача:");
        Console.Write("Введiть логiн: ");
        string username = Console.ReadLine();
        Console.Write("Введiть пароль: ");
        string password = Console.ReadLine();

        var (registeredHash, registeredSalt) = SaltedHash.HashPassword(password);
        Console.WriteLine($"Користувач зареєстрований. Хеш пароля: {registeredHash}, Сiль: {registeredSalt}");

        Console.Write("Введiть пароль для автентифiкацiї: ");
        string inputPassword = Console.ReadLine();

        if (SaltedHash.VerifyPassword(inputPassword, registeredHash, registeredSalt))
        {
            Console.WriteLine("Автентифiкацiя пройшла успiшно.");
        }
        else
        {
            Console.WriteLine("Помилка автентифiкацiї. Невiрний пароль.");
        }
    }
}

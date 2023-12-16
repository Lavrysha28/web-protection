using System;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        // iнiцiалiзацiя генератора криптографiчно стiйких випадкових чисел
        using (RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider())
        {
            // Генерацiя та виведення 10 криптографiчно стiйких випадкових чисел
            Console.WriteLine("Криптографiчно стiйка послiдовнiсть випадкових чисел:");

            for (int i = 0; i < 10; i++)
            {
                byte[] randomNumber = new byte[4]; // Наприклад, 4 байти для отримання 32 бiт
                rngCryptoServiceProvider.GetBytes(randomNumber);

                int randomInt = BitConverter.ToInt32(randomNumber, 0);
                Console.WriteLine(randomInt);
            }
        }
    }
}

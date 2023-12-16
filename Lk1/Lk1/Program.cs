using System;

class Program
{
    static void Main()
    {
        // iнiцiалiзацiя генератора псевдовипадкових чисел
        Random random = new Random();

        // Генерацiя та виведення 10 псевдовипадкових чисел
        Console.WriteLine("Послiдовнiсть псевдовипадкових чисел:");

        for (int i = 0; i < 10; i++)
        {
            int randomNumber = random.Next();
            Console.WriteLine(randomNumber);
        }
    }
}

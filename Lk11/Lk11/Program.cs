﻿using System;
using System.Collections.Generic;

// Клас, який представляє користувача
class Користувач
{
    public string Логiн { get; set; }
    public string Пароль { get; set; }
    public string Роль { get; set; }
}

// Клас, який вiдповiдає за автентифiкацiю та доступ
class СистемаУправлiнняДоступом
{
    private readonly List<Користувач> _користувачi;

    public СистемаУправлiнняДоступом()
    {
        _користувачi = new List<Користувач>();
    }

    // Реєстрацiя нового користувача
    public void Зареєструвати(Користувач користувач)
    {
        _користувачi.Add(користувач);
    }

    // Автентифiкацiя користувача та перевiрка доступу
    public bool Автентифiкацiя(string логiн, string пароль, string роль)
    {
        var користувач = _користувачi.Find(u => u.Логiн == логiн && u.Пароль == пароль && u.Роль == роль);

        if (користувач != null)
        {
            Console.WriteLine($"Користувач {логiн} автентифiкований з роллю {роль}.");
            ВиконатиДiїЗаРоллю(роль);
            return true;
        }

        Console.WriteLine($"Невдала автентифiкацiя для користувача {логiн}.");
        return false;
    }

    // Метод, який викликається пiсля автентифiкацiї в залежностi вiд ролi
    private void ВиконатиДiїЗаРоллю(string роль)
    {
        switch (роль)
        {
            case "Адмiнiстратор":
                Console.WriteLine("Ви маєте права адмiнiстратора.");
                break;
            case "Менеджер":
                Console.WriteLine("Ви маєте права менеджера.");
                break;
            case "Користувач":
                Console.WriteLine("Ви маєте права користувача.");
                break;
            default:
                Console.WriteLine("Невiдома роль.");
                break;
        }
    }
}

class Програма
{
    static void Main()
    {
        // Створюємо систему управлiння доступом
        var система = new СистемаУправлiнняДоступом();

        // Реєстрацiя користувачiв
        система.Зареєструвати(new Користувач { Логiн = "admin", Пароль = "adminPass", Роль = "Адмiнiстратор" });
        система.Зареєструвати(new Користувач { Логiн = "manager", Пароль = "managerPass", Роль = "Менеджер" });
        система.Зареєструвати(new Користувач { Логiн = "user", Пароль = "userPass", Роль = "Користувач" });
        система.Зареєструвати(new Користувач { Логiн = "guest", Пароль = "guestPass", Роль = "Гiсть" });

        // Приклад автентифiкацiї
        система.Автентифiкацiя("admin", "adminPass", "Адмiнiстратор");
        система.Автентифiкацiя("manager", "wrongPassword", "Менеджер");
        система.Автентифiкацiя("user", "userPass", "Користувач");
        система.Автентифiкацiя("guest", "guestPass", "Гiсть");
    }
}
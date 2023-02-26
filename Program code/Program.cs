using System;
using System.Linq;

struct Record
{
    public string Name;
    public int Age;
    public string Gender;
    public string Address;
}

struct Log
{
    public enum ActionType { ADD, DELETE, UPDATE }
    public ActionType Action;
    public DateTime Time;
    public Record Record;
}

class Program
{
    static void Main(string[] args)
    {
        Record[] records = new Record[100];
        int recordCount = 0;
        Log[] log = new Log[50];
        int logCount = 0;
        DateTime lastActionTime = DateTime.Now;

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 – Просмотр таблицы");
            Console.WriteLine("2 – Добавить запись");
            Console.WriteLine("3 – Удалить запись");
            Console.WriteLine("4 – Обновить запись");
            Console.WriteLine("5 – Поиск записей");
            Console.WriteLine("6 – Просмотреть лог");
            Console.WriteLine("7 - Выход");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Таблица записей:");
                    Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-20}", "Имя", "Возраст", "Пол", "Адрес");
                    for (int i = 0; i < recordCount; i++)
                    {
                        Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-20}", records[i].Name, records[i].Age, records[i].Gender, records[i].Address);
                    }
                    break;

                case 2:
                    Console.Write("Введите имя: ");
                    string name = Console.ReadLine();
                    Console.Write("Введите возраст: ");
                    int age;
                    while (!int.TryParse(Console.ReadLine(), out age) || age < 0 || age > 150)
                    {
                        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    }
                    Console.Write("Введите пол: ");
                    string gender = Console.ReadLine();
                    Console.Write("Введите адрес: ");
                    string address = Console.ReadLine();

                    records[recordCount] = new Record { Name = name, Age = age, Gender = gender, Address = address };
                    recordCount++;

                    log[logCount] = new Log { Action = Log.ActionType.ADD, Time = DateTime.Now, Record = records[recordCount - 1] };
                    logCount++;

                    Console.WriteLine("Запись добавлена.");
                    break;

                case 3:
                    Console.Write("Введите номер записи для удаления: ");
                    int index;
                    while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > recordCount)
                    {
                        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    }
                    index--;

                    log[logCount] = new Log { Action = Log.ActionType.DELETE, Time = DateTime.Now, Record = records[index] };
                    logCount++;

                    for (int i = index; i < recordCount - 1; i++)
                    {
                        records[i] = records[i + 1];

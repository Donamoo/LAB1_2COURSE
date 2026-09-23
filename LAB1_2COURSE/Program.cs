using BusinessLogical;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ConsoleApp
{
    /// <summary>
    /// Консольное представление приложения «Работники предприятия».
    /// </summary>
    internal class Program
    {
        private static Logic logic = new Logic();
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                PrintMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddWorker(); break;
                    case "2": DeleteWorker(); break;
                    case "3": ShowAllWorkers(); break;
                    case "4": UpdateWorker(); break;
                    case "5": CountDepartament(); break;
                    case "6": ShowAvgSalary(); break;
                    default: Console.WriteLine("Неверный пункт меню."); break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Печатает главное меню.
        /// </summary>
        static void PrintMenu()
        {
            Console.WriteLine("=== Работники предприятия ===");
            Console.WriteLine("1. Добавить работника");
            Console.WriteLine("2. Удалить работника");
            Console.WriteLine("3. Показать всех работников");
            Console.WriteLine("4. Изменить работника");
            Console.WriteLine("5. Количество работников по отделам");
            Console.WriteLine("6. Средняя зарплата по отделам");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");
        }


        /// <summary>
        /// Добавляет работника: запрашивает поля, проверяет корректность ввода, вызывает <see cref="Logic.AddWorker"/>.
        /// </summary>
        /// <summary>
        /// Добавляет работника: запрашивает поля, проверяет их,
        /// вызывает <see cref="Logic.AddWorker"/>.
        /// </summary>
        static void AddWorker()
        {

            Console.Write("Введите ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("ID должен быть положительным числом.");
                return;
            }

            Console.Write("Введите ФИО: ");
            string name = (Console.ReadLine() ?? "").Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("ФИО не может быть пустым.");
                return;
            }
            if (!name.All(c => char.IsLetter(c) || c == ' '))
            {
                Console.WriteLine("ФИО должно содержать только буквы.");
                return;
            }

           
            Console.Write("Введите отдел: ");
            string department = (Console.ReadLine() ?? "").Trim();
            if (string.IsNullOrWhiteSpace(department))
            {
                Console.WriteLine("Отдел не может быть пустым.");
                return;
            }

            Console.Write("Введите зарплату: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal salary) || salary <= 0)
            {
                Console.WriteLine("Зарплата должна быть положительным числом.");
                return;
            }

            if (!logic.AddWorker(id, name, department, salary))
            {
                Console.WriteLine("Работник с таким ID уже существует.");
                return;
            }

            Console.WriteLine("Работник добавлен.");
        }

        /// <summary>
        /// Удаляет работника по ID.
        /// </summary>
        static void DeleteWorker()
        {
            int id = FoolCheckInt("Введите ID для удаления: ");
            if (id <= 0) return;

            if (logic.DeleteWorker(id))
                Console.WriteLine("Работник удалён.");
            else
                Console.WriteLine("Работник с таким ID не найден.");
        }

        /// <summary>
        /// Выводит список всех работников.
        /// </summary>
        static void ShowAllWorkers()
        {
            List<Worker> workers = logic.GetAll();

            if (workers.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            Console.WriteLine($"{"ID",-5} {"ФИО",-25} {"Отдел",-15} {"Зарплата",10}");
            foreach (var w in workers)
            {
                Console.WriteLine($"{w.ID,-5} {w.Name,-25} {w.Department,-15} {w.Salary,10:F2}");
            }
        }

        /// <summary>
        /// Изменяет работника: сначала по ID находит его, потом запрашивает новые поля.
        /// </summary>
        /// <summary>
        /// Изменяет работника: сначала по ID находит его,
        /// затем запрашивает новые поля,
        /// проверяет их и передаёт в <see cref="Logic.UpdateWorker"/>.
        /// </summary>
        static void UpdateWorker()
        {
            int id = FoolCheckInt("Введите ID работника для изменения: ");
            if (id <= 0) return;

            Worker existing = logic.GetAll().FirstOrDefault(w => w.ID == id);
            if (existing == null)
            {
                Console.WriteLine("Работник с таким ID не найден.");
                return;
            }

            Console.WriteLine("Введите новые данные:");

            
            Console.Write($"ФИО [{existing.Name}]: ");
            string nameInput = Console.ReadLine();
            string name = string.IsNullOrWhiteSpace(nameInput) ? existing.Name : nameInput.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("ФИО не может быть пустым.");
                return;
            }
            if (!name.All(c => char.IsLetter(c) || c == ' '))
            {
                Console.WriteLine("ФИО должно содержать только буквы.");
                return;
            }

            
            Console.Write($"Отдел [{existing.Department}]: ");
            string depInput = Console.ReadLine();
            string department = string.IsNullOrWhiteSpace(depInput) ? existing.Department : depInput.Trim();

            if (string.IsNullOrWhiteSpace(department))
            {
                Console.WriteLine("Отдел не может быть пустым.");
                return;
            }

            Console.Write($"Зарплата [{existing.Salary}]: ");
            string salInput = Console.ReadLine();
            decimal salary;
            if (string.IsNullOrWhiteSpace(salInput))
            {
                salary = existing.Salary;
            }
            else if (!decimal.TryParse(salInput, out salary) || salary <= 0)
            {
                Console.WriteLine("Зарплата должна быть положительным числом.");
                return;
            }

            if (logic.UpdateWorker(id, name, department, salary))
                Console.WriteLine("Данные обновлены.");
            else
                Console.WriteLine("Не удалось обновить работника.");
        }


        /// <summary>
        /// Бизнес-функция: количество работников в каждом отделе.
        /// </summary>
        static void CountDepartament()
        {
            Dictionary<string, List<Worker>> groups = logic.GroupByDepartment();

            if (groups.Count == 0)
            {
                Console.WriteLine("Нет данных.");
                return;
            }

            Console.WriteLine($"{"Отдел",-20} {"Количество",10}");
            foreach (var pair in groups)
            {
                Console.WriteLine($"{pair.Key,-20} {pair.Value.Count,10}");
            }
        }

        /// <summary>
        /// Бизнес-функция: средняя зарплата по отделам.
        /// </summary>
        static void ShowAvgSalary()
        {
            Dictionary<string, decimal> averages = logic.AvgSalary();

            if (averages.Count == 0)
            {
                Console.WriteLine("Нет данных.");
                return;
            }

            Console.WriteLine($"{"Отдел",-20} {"Средняя ЗП",12}");
            foreach (var pair in averages)
            {
                Console.WriteLine($"{pair.Key,-20} {pair.Value,12:F2}");
            }
        }

        /// <summary>
        /// Запрашивает положительное целое число. Возвращает 0, если ввод некорректен.
        /// </summary>
        static int FoolCheckInt(string prompt)
        {
            Console.Write(prompt);
            if (!int.TryParse(Console.ReadLine(), out int value) || value <= 0)
            {
                Console.WriteLine("Некорректное значение ID.");
                return 0;
            }
            return value;
        }
    }
}
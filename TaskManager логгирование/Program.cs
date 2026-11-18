using System;
using System.Collections.Generic;
using System.IO;

namespace TaskManagerApp
{
    public static class Logger
    {
        private static readonly string LogFilePath = "app.log";

        static Logger()
        {
            File.WriteAllText(LogFilePath, string.Empty);
        }

        private static void WriteLog(string level, string message)
        {
            string logEntry = $"[{level}] {message}";
            Console.WriteLine(logEntry);
            File.AppendAllText(LogFilePath, logEntry + Environment.NewLine);
        }

        public static void Info(string message) => WriteLog("INFO", message);
        public static void Trace(string message) => WriteLog("TRACE", message);
        public static void Error(string message) => WriteLog("ERROR", message);
    }

    class Program
    {
        private static readonly List<string> Tasks = new List<string>();

        static void Main(string[] args)
        {
            Logger.Info("Программа TaskManager запущена.");

            bool isRunning = true;
            while (isRunning)
            {
                Console.Write("\nВведите команду (add, remove, list, exit): ");
                string? command = Console.ReadLine()?.Trim().ToLower();

                switch (command)
                {
                    case "add":
                        AddTask();
                        break;
                    case "remove":
                        RemoveTask();
                        break;
                    case "list":
                        ListTasks();
                        break;
                    case "exit":
                        Logger.Info("Завершение работы программы.");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда. Доступны: add, remove, list, exit.");
                        break;
                }
            }
        }

        private static void AddTask()
        {
            Console.Write("Введите название задачи: ");
            string? taskName = Console.ReadLine()?.Trim();

            Logger.Trace("Начало операции AddTask.");

            if (!string.IsNullOrEmpty(taskName))
            {
                Tasks.Add(taskName);
                Logger.Info($"Задача \"{taskName}\" успешно добавлена.");
            }
            else
            {
                Logger.Error("Название задачи не может быть пустым.");
            }

            Logger.Trace("Конец операции AddTask.");
        }

        private static void RemoveTask()
        {
            Console.Write("Введите название задачи для удаления: ");
            string? taskName = Console.ReadLine()?.Trim();

            Logger.Trace("Начало операции RemoveTask.");

            if (!string.IsNullOrEmpty(taskName) && Tasks.Contains(taskName))
            {
                Tasks.Remove(taskName);
                Logger.Info($"Задача \"{taskName}\" успешно удалена.");
            }
            else
            {
                Logger.Error($"Задача \"{taskName}\" не найдена.");
            }

            Logger.Trace("Конец операции RemoveTask.");
        }

        private static void ListTasks()
        {
            Logger.Trace("Начало операции ListTasks.");
            Logger.Info($"Всего задач: {Tasks.Count}.");

            if (Tasks.Count == 0)
            {
                Console.WriteLine("Список задач пуст.");
            }
            else
            {
                for (int i = 0; i < Tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Tasks[i]}");
                }
            }

            Logger.Trace("Конец операции ListTasks.");
        }
    }
}
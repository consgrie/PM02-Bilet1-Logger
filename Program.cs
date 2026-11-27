using System;
using System.IO;
using System.Text;

enum LogLevel { Info, Warning, Error }

sealed class Logger
{
    // Единственный экземпляр логгера.
    public static Logger Instance { get; } = new Logger();

    private Logger() { }

    public void Log(string message, LogLevel level = LogLevel.Info, string? path = null)
    {
        // Добавляем время и уровень сообщения.
        string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level.ToString().ToUpperInvariant()}] {message}";
        Console.WriteLine(line);

        // Если указан файл, дописываем запись в конец.
        if (path != null)
            File.AppendAllText(path, line + Environment.NewLine, Encoding.UTF8);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Logger first = Logger.Instance;
        Logger second = Logger.Instance;

        // Сравниваем ссылки на логгер.
        Console.WriteLine($"Один экземпляр: {ReferenceEquals(first, second)}");
        string? path = Array.IndexOf(args, "--file") >= 0 ? "events.log" : null;
        first.Log("Программа запущена", LogLevel.Info, path);
        second.Log("Пример предупреждения", LogLevel.Warning, path);
        first.Log("Пример ошибки", LogLevel.Error, path);
    }
}

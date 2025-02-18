using DatabaseLibrary.Enums;
using DatabaseLibrary.Models;
using DatabaseLibrary.Services;

namespace ConsoleApp.Services;

public class MenuService : IMenuService
{
    private DatabaseService Service { get; set; } = new();

    /// <summary>
    /// Старт программы
    /// </summary>
    /// <param name="args">Аргументы строки</param>
    public void Start(String[] args)
    {
        try
        {
            switch (args[1])
            {
                case "install":
                    if (args.Length > 2)
                        Install(args[2]);
                    else
                        Console.WriteLine("Неверный вызов команды. dotnet run install [название базы]");
                    break;
                case "view":
                    ViewData();
                    break;
                case "delete":
                    if (args.Length > 2)
                        DeleteData(args[2]);
                    else
                        Console.WriteLine("Неверный вызов команды. dotnet run delete [guid]");
                    break;
                case "import":
                    if (args.Length > 2)
                        ImportData(args[2]);
                    else
                        Console.WriteLine("Неверный вызов команды. dotnet run import [путь к файлу]");
                    break;
                case "post":
                    if (args.Length > 2)
                        PostData(args[2]);
                    else
                        Console.WriteLine("Неверный вызов команды. dotnet run post [guid]");
                    break;
                case "--info":
                    ShowInfo();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Не удалось выполнить, поскольку указанная команда не найдена.");
                    Console.ResetColor();
                    Console.WriteLine("Для просмотра списка команд используйте --info.");
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }


    /// <summary>
    /// Вывод информации о командах консоли
    /// </summary>
    private static void ShowInfo()
    {
        Console.WriteLine("dotnet run [команда]");
        Console.WriteLine("\t install [название базы] - инициализировать базу данных");
        Console.WriteLine("\t view - ");
        Console.WriteLine("\t delete - ");
        Console.WriteLine("\t import - ");
        Console.WriteLine("\t post - ");
    }


    /// <summary>
    /// Инициализация базы данных
    /// </summary>
    /// <param name="name">Название базы</param>
    public void Install(String name)
    {
        bool complete = false;
        try
        {
            string path = Path.Combine(Service.GetBasePath(), $"{name}.db");


            if (File.Exists(path))
            {
                Console.WriteLine("База данных уже существует, хотите перезаписать её?");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t\t[Y] да");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\t[N] нет\n");
                Console.ResetColor();

                String? answer = Console.ReadLine()?.ToLower();
                if (answer is "y")
                {
                    File.Delete(path);
                    complete = Service.CreateTables();
                    File.WriteAllText(Path.Combine(Service.GetBasePath(), "database.txt"), $"{name}.db");
                }
            }
            else
            {
                File.WriteAllText(Path.Combine(Service.GetBasePath(), "database.txt"), $"{name}.db");
                complete = Service.CreateTables();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            String message = complete ? "База создана!" : "База не создана!";
            Console.WriteLine(message);
        }
    }

    /// <summary>
    /// Метод вывода всех данных из базы
    /// </summary>
    private void ViewData()
    {
        if (Authorization())
        {
            List<Order> orders = Service.View();
            if (orders.Count == 0)
            {
                Console.WriteLine("Данных еще нет!");
                return;
            }

            List<IGrouping<Transaction, Order>> groupedList = orders.GroupBy(c => c.Transaction).ToList()!;
            for (int i = 0; i < groupedList.Count; i++)
            {
                Console.WriteLine(groupedList.ElementAt(i).Key.ToString());
                for (int j = 0; j < groupedList.ElementAt(i).Count(); j++)
                {
                    Console.WriteLine(groupedList.ElementAt(i).ElementAt(j).ToString());
                }
            }
        }
    }

    /// <summary>
    /// Удаление данных
    /// </summary>
    /// <param name="guid">Идентификатор записи</param>
    private void DeleteData(string guid)
    {
        bool complete = false;
        try
        {
            if (Authorization())
            {
                complete = Service.Delete(guid);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            String message = complete ? "Данные удалены!" : "Данные не удалены!";
            Console.WriteLine(message);
        }
    }

    /// <summary>
    /// Импорт данных
    /// </summary>
    /// <param name="path">Путь к файлу</param>
    private void ImportData(string path)
    {
        bool complete = false;
        try
        {
            if (Authorization())
            {
                if (File.Exists(path))
                {
                    FileInfo info = new FileInfo(path);
                    Char? separator;

                    switch (info.Extension)
                    {
                        case ".txt":
                            Console.Write("Введите символ разделитель: ");
                            separator = (Console.ReadLine() ?? String.Empty)[0];
                            complete = Service.Import(path, FileExtension.Txt, separator);
                            break;
                        case ".csv":
                            Console.Write("Введите символ разделитель: ");
                            separator = (Console.ReadLine() ?? String.Empty)[0];
                            complete = Service.Import(path, FileExtension.Csv, separator);
                            break;
                        case ".json":
                            complete = Service.Import(path, FileExtension.Json);
                            break;
                        case ".xml":
                            complete = Service.Import(path, FileExtension.Xml);
                            break;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //todo: сделать логи ошибок
        }
        finally
        {
            String message = complete ? "Данные импортированы!" : "Данные не импортированы!";
            Console.WriteLine(message);
        }
    }

    private void PostData(string guid)
    {
        if (Authorization())
        {
        }
    }

    public bool Authorization()
    {
        return true;
    }
}
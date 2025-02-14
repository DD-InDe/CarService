using DatabaseLibrary.Models;
using DatabaseLibrary.Services;

namespace ConsoleApp.Services;

public class MenuService : IMenuService
{
    DatabaseService service;

    public void Start(String[] args)
    {
        try
        {
            service = new();

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

    private static void ShowInfo()
    {
        Console.WriteLine("dotnet run [команда]");
        Console.WriteLine("\t install [название базы] - инициализировать базу данных");
        Console.WriteLine("\t view - ");
        Console.WriteLine("\t delete - ");
        Console.WriteLine("\t import - ");
        Console.WriteLine("\t post - ");
    }

    public bool Install(String name)
    {
        try
        {
            string path = Path.Combine(service.GetBasePath(), $"{name}.db");
            

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
                    service.CreateTables();
                    File.WriteAllText(Path.Combine(service.GetBasePath(), "database.txt"), $"{name}.db");
                }
            }
            else
            {
                File.WriteAllText(Path.Combine(service.GetBasePath(), "database.txt"), $"{name}.db");
                service.CreateTables();
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    private void ViewData()
    {
        if (Authorization())
        {
            List<Order> orders = service.View();
            if (orders.Count == 0)
            {
                Console.WriteLine("Данных еще нет!");
                return;
            }

            List<IGrouping<String, Order>> groupedList = orders.GroupBy(c => c.Guid).ToList();
            for (int i = 0; i < groupedList.Count; i++)
            {
                // Order order = (Order)groupedList.ElementAt(i);
                // Console.WriteLine($"\t{order.z}|{grouping.ElementAt()}");
            }
        }
    }

    private void DeleteData(string guid)
    {
        if (Authorization())
        {
        }
    }

    private bool ImportData(string path)
    {
        try
        {
            if (Authorization())
            {
                if (File.Exists(path))
                {
                    FileInfo info = new FileInfo(path);
                    Char separator;

                    switch (info.Extension)
                    {
                        case ".txt":
                        case ".csv":
                            Console.Write("Введите символ разделитель: ");
                            separator = (Console.ReadLine() ?? String.Empty)[0];
                            break;
                        case ".json":
                            break;
                        case ".xml":
                            break;
                    }

                    if (info.Extension == ".txt" || info.Extension == ".csv")
                    {
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //todo: сделать логи ошибок
        }

        return false;
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
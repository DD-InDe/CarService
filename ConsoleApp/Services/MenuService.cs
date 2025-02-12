using DatabaseLibrary.Services;

namespace ConsoleApp.Services;

public class MenuService : IMenuService
{
    public void Start(String[] args)
    {
        try
        {
            DatabaseService service = new();
            switch (args[1])
            {
                case "install":
                    if (args.Length > 2)
                        service.Install(args[2]);
                    else
                        Console.WriteLine("Неверный вызов команды. dotnet run install [название базы]");
                    break;
                case "view":
                    service.View();
                    break;
                case "delete":
                    service.Delete();
                    break;
                case "import":
                    service.Import();
                    break;
                case "post":
                    service.Post();
                    break;
                case "--info":
                    Console.WriteLine("dotnet run [команда]");
                    Console.WriteLine("\t install [название базы] - инициализировать базу данных");
                    Console.WriteLine("\t view - ");
                    Console.WriteLine("\t delete - ");
                    Console.WriteLine("\t import - ");
                    Console.WriteLine("\t post - ");
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
}
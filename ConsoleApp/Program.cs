using ConsoleApp.Services;

String[] arguments = Environment.GetCommandLineArgs();

if(arguments.Length == 1) return ;

DatabaseService service = new();
switch (arguments[1])
{
    case "install":
        if (arguments.Length > 2)
            service.Install(arguments[2]);
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
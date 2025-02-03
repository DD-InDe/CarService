using Microsoft.Data.Sqlite;

namespace ConsoleApp.Services;

public class DatabaseService : IDatabaseService
{
    private String _login;
    private String _password;

    public void Install(String name)
    {
        try
        {
            String connectionString = $"Data Source={name}.db";

            if (File.Exists($"{name}.db"))
            {
                Console.WriteLine("База данных уже существует, хотите перезаписать её?");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t\t[Y] да");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\t[N] нет\n");
                Console.ResetColor();

                String? answer = Console.ReadLine();
                if (answer is "y" or "Y")
                    CreateTables(connectionString);
            }
            else
                CreateTables(connectionString);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void CreateTables(String connectionString)
    {
        try
        {
            using var connection = new SqliteConnection(connectionString);

            connection.Open();
            SqliteCommand command;
            command = new()
            {
                CommandText = "create table users(" +
                              "id integer not null primary key autoincrement unique," +
                              "login text," +
                              "password text)",
                Connection = connection
            };
            command.ExecuteNonQuery();

            command = new()
            {
                CommandText = "create table transaction(" +
                              "id text,",
                Connection = connection
            };
            command.ExecuteNonQuery();

            command = new()
            {
                CommandText = "insert into users (login, password) values ('admi@admin.ru','admin')",
                Connection = connection
            };
            command.ExecuteNonQuery();
            connection.Close();

            _login = "admi@admin.ru";
            _password = "admind";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("База создана!");
            Console.ResetColor();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void View()
    {
    }

    public void Delete()
    {
    }

    public void Import()
    {
    }

    public void Post()
    {
    }
}
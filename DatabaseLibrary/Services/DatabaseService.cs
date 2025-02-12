using Microsoft.Data.Sqlite;

namespace DatabaseLibrary.Services;

public class DatabaseService : IDatabaseService
{
    #region переменные

    private String _login;
    private String _password;

    #endregion

    public void Install(String name)
    {
        try
        {
            String connectionString = $"Data Source={name}.db";

            string path = $"{name}.db";
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
                    CreateTables(connectionString);
                }
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
        using var connection = new SqliteConnection(connectionString);

        connection.Open();
        SqliteCommand command;

        // создание таблицы Users
        command = new()
        {
            CommandText = "create table users(" +
                          "id integer not null primary key autoincrement unique," +
                          "login text," +
                          "password text)",
            Connection = connection
        };
        command.ExecuteNonQuery();


        // заполнение таблицы Users
        command = new()
        {
            CommandText = "insert into users (login, password) values ('admi@admin.ru','admin')",
            Connection = connection
        };
        command.ExecuteNonQuery();

        _login = "admi@admin.ru";
        _password = "admin";


        // создание таблицы Transaction
        command = new()
        {
            CommandText = "create table transaction(" +
                          "guid text primary key unique ," +
                          "date_time text, " +
                          "table_name text" +
                          "rows_count int)",
            Connection = connection
        };
        command.ExecuteNonQuery();

        // создание таблицы Order
        command = new()
        {
            CommandText = "create table [order](" +
                          "id int primary key autoincrement unique," +
                          "client_fullname text," +
                          "date_create text," +
                          "date_complete text," +
                          "car_brand text," +
                          "car_model text," +
                          "gov_number text," +
                          "car_vin text," +
                          "employee_fullname text," +
                          "services text," +
                          "materials text," +
                          "client_materials text," +
                          "guid text foreign key references [transaction](guid) on delete cascade)",
            Connection = connection
        };
        command.ExecuteNonQuery();

        connection.Close();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("База создана!");
        Console.ResetColor();
    }

    public void View()
    {
        if (Authorization())
        {
            
        }
    }

    public void Delete()
    {
        if (Authorization())
        {
        }
    }

    public void Import()
    {
    }

    public void Post()
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
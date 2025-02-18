using DatabaseLibrary.Enums;
using DatabaseLibrary.Models;
using Microsoft.Data.Sqlite;

namespace DatabaseLibrary.Services;

public class DatabaseService : IDatabaseService
{
    /// <summary>
    /// Метод для создания таблиц в базе данных
    /// </summary>
    public bool CreateTables()
    {
        try
        {
            using var connection = new SqliteConnection(GetConnectionString());
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


            // создание таблицы Transaction
            command = new()
            {
                CommandText = "create table [transaction](" +
                              "guid text primary key unique ," +
                              "date_time text, " +
                              "table_name text," +
                              "rows_count int)",
                Connection = connection
            };
            command.ExecuteNonQuery();

            // создание таблицы Order
            command = new()
            {
                CommandText = "create table [order](" +
                              "id integer not null primary key autoincrement unique," +
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
                              "guid text)",
                Connection = connection
            };
            command.ExecuteNonQuery();

            connection.Close();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Метод для просмотра данных из таблиц
    /// </summary>
    public List<Order> View()
    {
        using var connection = new SqliteConnection(GetConnectionString());
        connection.Open();

        List<Transaction> transactions = new();
        List<Order> orders = new();

        SqliteCommand command;

        // получение данных из таблицы transaction
        command = new SqliteCommand()
        {
            CommandText = "select * from \"transaction\"",
            Connection = connection
        };
        using (SqliteDataReader reader = command.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    transactions.Add(new()
                    {
                        Guid = (String)reader.GetValue(0),
                        DateTime = Convert.ToDateTime((String)reader.GetValue(1)),
                        TableName = (String)reader.GetValue(2),
                        RowsCount = Convert.ToInt32(reader.GetValue(3)),
                    });
                }
            }
        }

        // получение данных из таблицы order
        command = new SqliteCommand()
        {
            CommandText = "select * from \"order\"",
            Connection = connection
        };
        using (SqliteDataReader reader = command.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Order order = new Order()
                    {
                        Id = Convert.ToInt32(reader.GetValue(0)),
                        ClientFullName = (String)reader.GetValue(1),
                        DateCreate = Convert.ToDateTime((String)reader.GetValue(2)),
                        DateComplete = Convert.ToDateTime((String)reader.GetValue(3)),
                        CarBrand = (String)reader.GetValue(4),
                        CarModel = (String)reader.GetValue(5),
                        GovNumber = (String)reader.GetValue(6),
                        CarVin = (String)reader.GetValue(7),
                        EmployeeFullName = (String)reader.GetValue(8),
                        Services = (String)reader.GetValue(9),
                        Materials = (String)reader.GetValue(10),
                        ClientMaterials = (String)reader.GetValue(11),
                        Guid = (String)reader.GetValue(12)
                    };

                    order.Transaction = transactions.First(c => c.Guid.Equals(order.Guid));
                    orders.Add(order);
                }
            }
        }

        connection.Close();
        return orders;
    }

    /// <summary>
    /// Метод для удаления данных из базы
    /// </summary>
    /// <param name="guid">Идентификатор записи, которую нужно удалить</param>
    /// <returns>Данные загружены</returns>
    public bool Delete(String guid)
    {
        try
        {
            using var connection = new SqliteConnection(GetConnectionString());
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "select guid from \"transaction\" where guid = @Guid";
            command.Parameters.AddWithValue("@Guid", guid);
            SqliteDataReader reader = command.ExecuteReader();
            Console.WriteLine(reader.HasRows);
            if (!reader.HasRows) return false;

            command = new();
            command.Connection = connection;
            command.CommandText = "delete from \"transaction\" where guid=@Guid";
            command.Parameters.AddWithValue("@Guid", guid);
            command.ExecuteNonQuery();

            command = new();
            command.Connection = connection;
            command.CommandText = "delete from \"order\" where guid=@Guid";
            command.Parameters.AddWithValue("@Guid", guid);
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Метод для импорта данных из файла
    /// </summary>
    /// <param name="path">Путь к файлу</param>
    /// <param name="separator">Символ-разделитель</param>
    /// <param name="extension">Расширение файла</param>
    /// <returns>Данные загружены</returns>
    public bool Import(String path, FileExtension extension, char? separator = null)
    {
        try
        {
            Transaction transaction = new()
                { Guid = Guid.NewGuid().ToString(), DateTime = DateTime.Now, TableName = "order" };
            ImportService service = new ImportService(path);

            List<Order> orders;
            switch (extension)
            {
                case FileExtension.Csv:
                    orders = service.FromCsv<Order>(separator!.Value);
                    break;
                case FileExtension.Json:
                    orders = service.FromJson<Order>();
                    break;
                case FileExtension.Txt:
                    orders = service.FromTxt<Order>(separator!.Value);
                    break;
                case FileExtension.Xml:
                    orders = service.FromXml<OrderList>().Orders;
                    break;
                default:
                    return false;
            }

            if (orders.Count == 0) return false;

            transaction.RowsCount = orders.Count;

            using var connection = new SqliteConnection(GetConnectionString());
            connection.Open();

            SqliteCommand transactionCommand = new SqliteCommand()
            {
                CommandText =
                    $"insert into \"transaction\" (guid,date_time,table_name,rows_count)" +
                    $"values (\"{transaction.Guid}\"," +
                    $"\"{transaction.DateTime:d}\"," +
                    $"\"{transaction.TableName}\"," +
                    $"\"{transaction.RowsCount}\")",
                Connection = connection
            };
            transactionCommand.ExecuteNonQuery();

            foreach (var order in orders)
            {
                order.Guid = transaction.Guid;

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "insert into \"order\" (client_fullname,date_create,date_complete,car_brand," +
                                      "car_model,gov_number,car_vin,employee_fullname,services,materials,client_materials,guid) " +
                                      "values (@Client,@DCreate,@DComplete,@Brand," +
                                      "@Model,@Number,@Vin,@Employee,@Services,@Materials,@CMaterials,@Guid)";
                command.Parameters.AddWithValue("@Client", order.ClientFullName);
                command.Parameters.AddWithValue("@DCreate", order.DateCreate.ToString("d"));
                command.Parameters.AddWithValue("@DComplete", order.DateComplete.ToString("d"));
                command.Parameters.AddWithValue("@Brand", order.CarBrand);
                command.Parameters.AddWithValue("@Model", order.CarModel);
                command.Parameters.AddWithValue("@Number", order.GovNumber);
                command.Parameters.AddWithValue("@Vin", order.CarVin);
                command.Parameters.AddWithValue("@Employee", order.EmployeeFullName);
                command.Parameters.AddWithValue("@Services", order.Services);
                command.Parameters.AddWithValue("@Materials", order.Materials);
                command.Parameters.AddWithValue("@CMaterials", order.ClientMaterials);
                command.Parameters.AddWithValue("@Guid", order.Guid);
                command.ExecuteNonQuery();
            }

            connection.Close();
            return true;
        }
        catch (Exception e)
        {
            if (!Directory.Exists("logs"))
                Directory.CreateDirectory("logs");
            DateTime dt = DateTime.Now;
            String fileName = $"{dt.ToString("yyyy-MM-dd")}_{dt.ToString("hh_mm")}_error.log";
            String logPath = Path.Combine("logs", fileName);
            File.WriteAllText(logPath, e.ToString());
            Console.WriteLine($"Ошибка импорта. Подробности в файле {logPath}");
            return false;
        }
    }

    public bool Post()
    {
        Console.WriteLine(GetConnectionString());
        return true;
    }

    private string GetConnectionString()
    {
        String path = Path.Combine(GetBasePath(), "database.txt");
        String source = Path.Combine(GetBasePath(), File.ReadAllText(path));
        return $"Data Source={source}";
    }

    public string GetBasePath() => Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
}
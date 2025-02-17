using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace DatabaseLibrary.Services;

internal class ImportService(String path) : IImportService
{
    private string _path = path;

    public List<T> FromJson<T>()
    {
        try
        {
            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
            String allText = File.ReadAllText(_path);

            List<T> list = JsonSerializer.Deserialize<List<T>>(allText, options) ?? new List<T>();
            return list;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public T FromXml<T>()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            Stream stream = File.OpenRead(_path);

            using var xmlReader = XmlReader.Create(stream);
            return (T)serializer.Deserialize(xmlReader)!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<T> FromTxt<T>(char separator) where T : IFillable, new()
    {
        try
        {
            return ParseWithSeparator<T>(separator);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<T> FromCsv<T>(char separator) where T : IFillable, new()
    {
        try
        {
            return ParseWithSeparator<T>(separator);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private List<T> ParseWithSeparator<T>(char separator) where T : IFillable, new()
    {
        try
        {
            List<String> allText = File.ReadAllLines(_path).ToList();

            if (allText.Count != 0)
            {
                List<T> list = new();

                foreach (var row in allText)
                {
                    String[] columns = row.Split(separator);

                    T obj = new T();
                    obj.FillFromColumns(columns);

                    list.Add(obj);
                }

                return list;
            }

            return new();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
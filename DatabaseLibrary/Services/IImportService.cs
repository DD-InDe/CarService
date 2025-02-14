using DatabaseLibrary.Models;

namespace DatabaseLibrary.Services;

public interface IImportService
{
    List<T> FromJson<T>();
    List<T> FromXml<T>();
    List<T> FromTxt<T>(Char separator) where T : IFillable, new();
    List<T> FromCsv<T>(Char separator) where T : IFillable, new();
}
using DatabaseLibrary.Enums;
using DatabaseLibrary.Models;

namespace DatabaseLibrary.Services;

public interface IDatabaseService
{
    public List<Order> View();
    public bool Delete(String guid);
    public bool Import(String path, FileExtension extension, Char? separator);
    public bool Post();
}
namespace ConsoleApp.Services;

public interface IDatabaseService
{
    void Install(String name);
    void View();
    void Delete();
    void Import();
    void Post();
}
namespace ConsoleApp.Services;

public interface IMenuService
{
    public void Start(String[] args);
    public bool Install(String databaseName);

    public bool Authorization();
}
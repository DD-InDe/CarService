namespace ConsoleApp.Services;

public interface IMenuService
{
    public void Start(String[] args);
    public void Install(String databaseName);

    public bool Authorization();
}
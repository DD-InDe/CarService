namespace DatabaseLibrary.Services;

public interface IFillable
{
    public void FillFromColumns(String[] columns);
}
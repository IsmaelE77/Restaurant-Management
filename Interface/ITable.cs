namespace Restaurant_Management.Interface;

public interface ITable : ICrud<Table>
{
    public bool IsAvailable(int Id);
    public IEnumerable<Table> GetAvailableTables();
}

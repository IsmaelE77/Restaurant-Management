namespace Restaurant_Management.Interface;

public interface ITable : ICRUD<Table>
{
    public bool IsAvailable(int Id);
    public IEnumerable<Table> GetAvailableTables();
}

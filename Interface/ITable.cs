namespace Restaurant_Management.Interface;

public interface ITable : ISpecial<Table>
{
    public bool IsAvailable(int Id);
    public IEnumerable<Table> GetAvailableTables();
}

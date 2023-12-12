namespace Restaurant_Management.Interface;

public interface ISpecial<T>
{
    public T? Get(int Id);
    public IEnumerable<T> GetAll();
    public int Add(T section);
    public int RemoveById(int Id);
    public int Remove(T section);
    public int Update(T section);
}

namespace Restaurant_Management.Interface;

public interface ICrud<T>
{
    public T? Get(int Id);
    public IEnumerable<T> GetAll();
    public bool Add(T section);
    public bool Remove(int Id);
    public bool Update(T section);
}

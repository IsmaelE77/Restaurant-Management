namespace Restaurant_Management.Interface;

public interface ICRUD<T>
{
    public T? Get(int Id);
    public IEnumerable<T> GetAll();
    public int Add(T section);
    public bool Remove(int Id);
    public bool Update(T section);
}
